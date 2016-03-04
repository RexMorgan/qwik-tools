using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Timers;
using qwik.coms.spotify.Configuration;
using qwik.coms.spotify.Errors;

namespace qwik.coms.spotify.Sessions
{
    public class Session : ISession
    {
        private readonly ISettings _settings;

        private delegate void LoggedInDelegate(IntPtr session, ErrorCode error);
        private delegate void LoggedOutDelegate(IntPtr session);
        private delegate void NotifyMainThreadDelegate(IntPtr session);
        private delegate int MusicDeliveryDelegate(IntPtr session, IntPtr format, IntPtr frames, int numFrames);
        private delegate void GetAudioBufferStatsDelegate(IntPtr sessionPtr, ref AudioBufferStats stats);
        private delegate void EndOfTrackDelegate(IntPtr session);

        private readonly LoggedInDelegate _loggedInDelegate;
        private readonly LoggedOutDelegate _loggedOutDelegate;
        private readonly NotifyMainThreadDelegate _notifyMainThreadDelegate;
        private readonly MusicDeliveryDelegate _musicDeliveryDelegate;
        private readonly GetAudioBufferStatsDelegate _getAudioBufferStatsDelegate;
        private readonly EndOfTrackDelegate _endOfTrackDelegate;

        public event Action<IntPtr, ErrorCode> OnLoggedIn;
        public event Action<IntPtr> OnLoggedOut;
        public event Action<AudioDataReceivedEventArgs> OnAudioDataReceived;
        public event Action<GetAudioBufferStatsEventArgs> OnGetAudioBufferStats;
        public event Action<TrackEndedEventArgs> OnEndOfTrack;

        public IntPtr SessionPtr { get; private set; }
        
        private readonly Timer _timer = new Timer();
        private DateTime _nextProcessEventDateTime = DateTime.Now;
        private bool _immediatelyProcessEvents;

        public Session(ISettings settings)
        {
            _settings = settings;

            _loggedInDelegate = LoggedInHandler;
            _loggedOutDelegate = LoggedOutHandler;
            _notifyMainThreadDelegate = NotifyMainThreadHandler;
            _musicDeliveryDelegate = MusicDeliveryHandler;
            _getAudioBufferStatsDelegate = GetAudioBufferStatsHandler;
            _endOfTrackDelegate = TrackEndedHandler;

            SessionPtr = CreateSession();

            _timer.Elapsed += (sender, arguments) =>
            {
                if (_nextProcessEventDateTime > DateTime.Now && !_immediatelyProcessEvents) return;

                lock (_timer)
                {
                    if (_nextProcessEventDateTime > DateTime.Now && !_immediatelyProcessEvents) return;

                    int nextTimeout;
                    Externals.sp_session_process_events(SessionPtr, out nextTimeout);
                    _nextProcessEventDateTime = DateTime.Now.AddMilliseconds(nextTimeout);
                    _immediatelyProcessEvents = false;
                }
            };

            _timer.AutoReset = true;
            _timer.Interval = 100;
            _timer.Start();
        }

        private IntPtr CreateSession()
        {
            var callbacks = new SessionCallbacks
            {
                LoggedIn = Marshal.GetFunctionPointerForDelegate(_loggedInDelegate),
                LoggedOut = Marshal.GetFunctionPointerForDelegate(_loggedOutDelegate),
                NotifyMainThread = Marshal.GetFunctionPointerForDelegate(_notifyMainThreadDelegate),
                MusicDelivery = Marshal.GetFunctionPointerForDelegate(_musicDeliveryDelegate),
                GetAudioBufferStats = Marshal.GetFunctionPointerForDelegate(_getAudioBufferStatsDelegate),
                EndOfTrack = Marshal.GetFunctionPointerForDelegate(_endOfTrackDelegate)
            };

            var callbacksPtr = Marshal.AllocHGlobal(Marshal.SizeOf(callbacks));
            Marshal.StructureToPtr(callbacks, callbacksPtr, true);

            var sessionConfig = new SessionConfig
            {
                ApiVersion = 12,
                UserAgent = "qwik.coms",
                ApplicationKeySize = _settings.SpotifyApplicationKey.Length,
                ApplicationKey = Marshal.AllocHGlobal(_settings.SpotifyApplicationKey.Length),
                CacheLocation = Path.Combine(Path.GetTempPath(), "spotify_cache"),
                SettingsLocation = Path.Combine(Path.GetTempPath(), "spotify_settings"),
                CompressPlaylists = true,
                DontSaveMetadataForPlaylists = false,
                InitiallyUnloadPlaylists = false,
                Callbacks = callbacksPtr
            };

            Marshal.Copy(_settings.SpotifyApplicationKey, 0, sessionConfig.ApplicationKey, _settings.SpotifyApplicationKey.Length);

            IntPtr sessionPtr;
            var error = Externals.sp_session_create(ref sessionConfig, out sessionPtr);
            if (error != ErrorCode.OK)
            {
                throw new SpotifyException(Externals.sp_error_message(error));
            }
            Externals.sp_session_set_connection_type(sessionPtr, ConnectionType.Wired);
            return sessionPtr;
        }

        public bool LoggedIn { get; private set; }

        public void Logout()
        {
            Externals.sp_session_logout(SessionPtr);
        }

        public void Login(string username, string password, bool rememberMe)
        {
            Externals.sp_session_login(SessionPtr, username, password, rememberMe, null);
        }

        public void Relogin()
        {
            var result = Externals.sp_session_relogin(SessionPtr);
            if (result != ErrorCode.OK) throw new SpotifyException(result);
        }

        public void Play(IntPtr track)
        {
            Externals.sp_session_player_load(SessionPtr, track);
            Externals.sp_session_player_play(SessionPtr, true);
        }

        public void Pause()
        {
            Externals.sp_session_player_play(SessionPtr, false);
        }

        public IEnumerable<PlaylistInfo> Playlists()
        {
            var containerPtr = Externals.sp_session_playlistcontainer(SessionPtr);
            var numPlaylists = Externals.sp_playlistcontainer_num_playlists(containerPtr);
            var playlists = new List<PlaylistInfo>();
            for (var i = 0; i < numPlaylists; ++i)
            {
                var playlistPtr = Externals.sp_playlistcontainer_playlist(containerPtr, i);
                playlists.Add(new PlaylistInfo(playlistPtr));
            }
            return playlists;
        }

        private void LoggedInHandler(IntPtr session, ErrorCode error)
        {
            if (error == ErrorCode.OK)
            {
                LoggedIn = true;
            }

            OnLoggedIn?.Invoke(session, error);
        }

        private void NotifyMainThreadHandler(IntPtr session)
        {
            _immediatelyProcessEvents = true;
        }

        private int MusicDeliveryHandler(IntPtr session, IntPtr formatPtr, IntPtr framesPtr, int numFrame)
        {
            if (numFrame == 0) return 0;

            var format = formatPtr.ToStructure<AudioFormat>();
            var buffer = new byte[numFrame * sizeof(Int16) * format.Channels];
            Marshal.Copy(framesPtr, buffer, 0, buffer.Length);

            if (OnAudioDataReceived == null) return numFrame;

            var bytesPerFrame = buffer.Length / numFrame;
            var eventArgs = new AudioDataReceivedEventArgs(session, format, numFrame, bytesPerFrame, buffer);
            OnAudioDataReceived(eventArgs);

            if (eventArgs.NumberOfFramesConsumed != 0) return eventArgs.NumberOfFramesConsumed;
            return numFrame;
        }

        private void GetAudioBufferStatsHandler(IntPtr sessionPtr, ref AudioBufferStats stats)
        {
            if (OnGetAudioBufferStats == null) return;

            var eventArgs = new GetAudioBufferStatsEventArgs(sessionPtr);
            OnGetAudioBufferStats(eventArgs);

            stats.Samples = eventArgs.Samples;
            stats.Stutter = eventArgs.Stutter;
        }

        private void TrackEndedHandler(IntPtr session)
        {
            OnEndOfTrack?.Invoke(new TrackEndedEventArgs(session));
        }

        private void LoggedOutHandler(IntPtr session)
        {
            OnLoggedOut?.Invoke(session);
        }
    }
}