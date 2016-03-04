using System;

namespace qwik.coms.spotify.Sessions
{
    public class TrackEndedEventArgs : EventArgs
    {
        public TrackEndedEventArgs(IntPtr sessionPtr)
        {
            SessionPtr = sessionPtr;
        }

        public IntPtr SessionPtr { get; }
    }
}