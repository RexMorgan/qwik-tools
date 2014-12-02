using System.IO;

namespace qwik.spotify.Configuration
{
    public class Settings : ISettings
    {
        private static readonly byte[] StaticSpotifyApplicationKey;

        static Settings()
        {
            try
            {
                StaticSpotifyApplicationKey = File.ReadAllBytes("spotify_appkey.key");
            }
            catch (IOException)
            {
                // TODO: Raise some event to notify things depending on this assembly that we couldn't find a valid key file.
            }
        }

        public byte[] SpotifyApplicationKey
        {
            get { return StaticSpotifyApplicationKey; }
        }
    }
}