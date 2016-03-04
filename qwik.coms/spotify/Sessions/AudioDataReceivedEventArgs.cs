using System;

namespace qwik.coms.spotify.Sessions
{
    public class AudioDataReceivedEventArgs : EventArgs
    {
        public AudioDataReceivedEventArgs(IntPtr session, AudioFormat format, int numberOfFrames, int bytesPerFrame, byte[] bytes)
        {
            Session = session;
            Format = format;
            NumberOfFrames = numberOfFrames;
            BytesPerFrame = bytesPerFrame;
            Bytes = bytes;
        }

        public IntPtr Session { get; private set; }
        public AudioFormat Format { get; private set; }
        public int NumberOfFrames { get; private set; }
        public int BytesPerFrame { get; private set; }
        public byte[] Bytes { get; private set; }
        public int NumberOfFramesConsumed { get; set; }
    }
}