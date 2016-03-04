using System;
using System.Collections.Generic;

namespace qwik.coms.spotify.Sessions
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

        public IntPtr PlaylistPtr { get; }
        public string Name { get; }
        public IEnumerable<TrackInfo> Tracks { get; } 
    }
}