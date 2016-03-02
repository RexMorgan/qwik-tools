using NAudio.Wave;
using qwik.spotify.Sessions;
using System;
using System.Collections.Generic;
using System.Linq;
using qwik.coms;
using qwik.coms.Output;
using qwik.helpers.Settings;

namespace qwik.spotify
{
    public interface IPlayer
    {
        void Play(IntPtr track);
        void Pause();

        TrackInfo Track(IntPtr track);
        IEnumerable<PlaylistInfo> Playlists();
        TrackInfo CurrentTrack { get; }
        PlaylistInfo CurrentPlaylist { get; }
    }

    public class Player : IPlayer
    {
        private readonly ISession _session;
        private readonly IEnumerable<INextTrackStrategy> _nextTrackStrategies;
        private readonly IAppSettings _appSettings;
        private readonly IOutput _output;

        private readonly IWavePlayer _wavePlayer;
        private readonly BufferedWaveProvider _waveProvider;

        private int _stutterCount;

        public Player(ISession session, IEnumerable<INextTrackStrategy> nextTrackStrategies, IAppSettings appSettings, IOutput output)
        {
            _session = session;
            _nextTrackStrategies = nextTrackStrategies;
            _appSettings = appSettings;
            _output = output;

            _lazyPlaylists = new Lazy<IEnumerable<PlaylistInfo>>(() => _session.Playlists());
            
            _wavePlayer = new WaveOut();
            _waveProvider = new BufferedWaveProvider(new WaveFormat())
            {
                BufferDuration = TimeSpan.FromSeconds(10)
            };

            _wavePlayer.Init(_waveProvider);

            _session.OnAudioDataReceived += SessionOnAudioDataReceived;
            _session.OnGetAudioBufferStats += SessionOnGetAudioBufferStats;
            _session.OnEndOfTrack += TrackEnded;
        }

        public void Play(IntPtr track)
        {
            _currentTrackPtr = track;
            _wavePlayer.Play();
            _session.Play(track);

            var currentTrack = CurrentTrack;
            _output.Output($"Playing {currentTrack.Name} by {currentTrack.Artist} [{currentTrack.Duration:mm\\:ss}]");
        }

        public void Pause()
        {
            _session.Pause();
        }

        public TrackInfo Track(IntPtr track)
        {
            return new TrackInfo(track);
        }

        private IntPtr _currentTrackPtr;
        public TrackInfo CurrentTrack => Track(_currentTrackPtr);

        private readonly Lazy<IEnumerable<PlaylistInfo>> _lazyPlaylists;

        public IEnumerable<PlaylistInfo> Playlists()
        {
            return _lazyPlaylists.Value;
        }

        public PlaylistInfo CurrentPlaylist => Playlists().SingleOrDefault(x => x.Name == _appSettings.SpotifyPlaylist);

        private void CheckStutter()
        {
            if (_waveProvider.BufferedBytes == 0) ++_stutterCount;
        }

        private void SessionOnAudioDataReceived(AudioDataReceivedEventArgs audioDataReceivedEventArgs)
        {
            CheckStutter();
            var bytesRemainingInBuffer = _waveProvider.BufferLength - _waveProvider.BufferedBytes;
            var amountToAddToBuffer = Math.Min(audioDataReceivedEventArgs.Bytes.Length, bytesRemainingInBuffer);
            _waveProvider.AddSamples(audioDataReceivedEventArgs.Bytes, 0, amountToAddToBuffer);
            audioDataReceivedEventArgs.NumberOfFramesConsumed = amountToAddToBuffer/audioDataReceivedEventArgs.BytesPerFrame;
        }

        private void SessionOnGetAudioBufferStats(GetAudioBufferStatsEventArgs audioBufferStatsEventArgs)
        {
            CheckStutter();
            audioBufferStatsEventArgs.Samples = _waveProvider.BufferedBytes/2;
            audioBufferStatsEventArgs.Stutter = _stutterCount;
            _stutterCount = 0;
        }

        private void TrackEnded(TrackEndedEventArgs trackEndedEventArgs)
        {
            var nextTrack = _nextTrackStrategies.Single(x => x.Enabled()).NextTrack(this);
            if (nextTrack == null) return;

            Play(nextTrack.TrackPtr);
        }
    }

    public interface INextTrackStrategy
    {
        bool Enabled();
        TrackInfo NextTrack(IPlayer player);
    }

    public class ShufflePlaylistNextTrackStrategy : INextTrackStrategy
    {
        public bool Enabled()
        {
            return true;
        }

        public TrackInfo NextTrack(IPlayer player)
        {
            return player.CurrentPlaylist.Tracks.Random();
        }
    }
}