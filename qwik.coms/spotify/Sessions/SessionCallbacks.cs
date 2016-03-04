using System;

namespace qwik.coms.spotify.Sessions
{
    public struct SessionCallbacks
    {
        public IntPtr LoggedIn;
        public IntPtr LoggedOut;
        public IntPtr MetadataUpdated;
        public IntPtr ConnectionError;
        public IntPtr MessageToUser;
        public IntPtr NotifyMainThread;
        public IntPtr MusicDelivery;
        public IntPtr PlayTokenLost;
        public IntPtr LogMessage;
        public IntPtr EndOfTrack;
        public IntPtr StreamingError;
        public IntPtr UserinfoUpdated;
        public IntPtr StartPlayback;
        public IntPtr StopPlayback;
        public IntPtr GetAudioBufferStats;
        public IntPtr OfflineStatusUpdated;
        public IntPtr OfflineError;
        public IntPtr CredentialsBlobUpdated;
        public IntPtr ConnectionstateUpdated;
        public IntPtr ScrobbleError;
        public IntPtr PrivateSessionModeChanged;
    }
}