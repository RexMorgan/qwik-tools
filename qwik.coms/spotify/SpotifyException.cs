using System;
using qwik.coms.spotify.Errors;

namespace qwik.coms.spotify
{
    public class SpotifyException : Exception
    {
        public IntPtr ErrorMessage { get; set; }
        public ErrorCode ErrorCode { get; set; }

        public SpotifyException(ErrorCode errorCode)
        {
            ErrorCode = errorCode;
        }

        public SpotifyException(IntPtr errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}