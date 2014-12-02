using System;
using System.Collections.Generic;
using qwik.spotify.Errors;

namespace qwik.spotify.Sessions
{
    public interface ISession
    {
        event Action<IntPtr, ErrorCode> OnLoggedIn;
        event Action<IntPtr> OnLoggedOut;
        event Action<AudioDataReceivedEventArgs> OnAudioDataReceived;
        event Action<GetAudioBufferStatsEventArgs> OnGetAudioBufferStats;
        event Action<TrackEndedEventArgs> OnEndOfTrack;

        IntPtr SessionPtr { get; }
        void Login(string username, string password, bool rememberMe);
        void Relogin();
        void Logout();
        void Play(IntPtr track);
        void Pause();

        IEnumerable<PlaylistInfo> Playlists();
    }
}