using System;
using System.Collections.Generic;
using System.Linq;

namespace qwik.spotify
{
    public class TrackInfo
    {
        public TrackInfo(IntPtr trackPtr)
        {
            TrackPtr = trackPtr;

            Name = Externals.sp_track_name(TrackPtr).ToStr();
            TrackNumber = Externals.sp_track_index(TrackPtr);
            AlbumPtr = Externals.sp_track_album(TrackPtr);

            var duration = Externals.sp_track_duration(TrackPtr);
            Duration = TimeSpan.FromMilliseconds(duration);

            var numArtists = Externals.sp_track_num_artists(TrackPtr);
            var artists = new List<string>();
            for (var i = 0; i < numArtists; ++i)
            {
                var artistPtr = Externals.sp_track_artist(TrackPtr, i);
                if (artistPtr == IntPtr.Zero) continue;
                artists.Add(Externals.sp_artist_name(artistPtr).ToStr());
            }
            Artists = artists;
        }

        public IntPtr TrackPtr { get; }

        public IEnumerable<string> Artists { get; }

        public string Artist => Artists.Any() ? Artists.First() : string.Empty;

        public string Name { get; }
        public TimeSpan Duration { get; }
        public int TrackNumber { get; }
        public IntPtr AlbumPtr { get; }
    }
}