using System;

namespace qwik.coms.spotify.Sessions
{
    public class GetAudioBufferStatsEventArgs : EventArgs
    {
        public GetAudioBufferStatsEventArgs(IntPtr session)
        {
            Session = session;
        }

        public IntPtr Session { get; private set; }
        public int Samples { get; set; }
        public int Stutter { get; set; }
    }
}