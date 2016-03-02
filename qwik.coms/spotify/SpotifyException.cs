using System;
using qwik.spotify.Errors;

namespace qwik.spotify
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