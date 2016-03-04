using System;

namespace qwik.coms.spotify.Sessions
{
    public struct SessionConfig
    {
        public int ApiVersion;
        public string CacheLocation;
        public string SettingsLocation;
        public IntPtr ApplicationKey;
        public int ApplicationKeySize;
        public string UserAgent;
        public IntPtr Callbacks;
        public IntPtr UserData;
        public bool CompressPlaylists;
        public bool DontSaveMetadataForPlaylists;
        public bool InitiallyUnloadPlaylists;
    }
}