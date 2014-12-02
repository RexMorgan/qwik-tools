using System;
using System.Collections.Generic;

namespace qwik.spotify.Sessions
{
    public class PlaylistInfo
    {
        public PlaylistInfo(IntPtr playlistPtr)
        {
            PlaylistPtr = playlistPtr;

            Name = Externals.sp_playlist_name(PlaylistPtr).ToStr();

            var numTracks = Externals.sp_playlist_num_tracks(PlaylistPtr);
            var tracks = new List<TrackInfo>();
            for (var i = 0; i < numTracks; ++i)
            {
                var trackPtr = Externals.sp_playlist_track(PlaylistPtr, i);
                tracks.Add(new TrackInfo(trackPtr));
            }
            Tracks = tracks;
        }

        public IntPtr PlaylistPtr { get; private set; }
        public string Name { get; private set; }
        public IEnumerable<TrackInfo> Tracks { get; private set; } 
    }
}