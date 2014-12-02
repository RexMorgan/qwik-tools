using qwik.spotify.Errors;
using qwik.spotify.Searches;
using qwik.spotify.Sessions;
using System;
using System.Runtime.InteropServices;

namespace qwik.spotify
{
    public static class Externals
    {
        /* Session */
        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_create(ref SessionConfig config, out IntPtr sessionPtr);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_set_connection_type(IntPtr sessionPtr, ConnectionType type);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_login(IntPtr sessionPtr, string username, string password, bool rememberMe, string blob);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_relogin(IntPtr sessionPtr);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_logout(IntPtr sessionPtr);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_process_events(IntPtr sessionPtr, out int nextTimeout);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_player_load(IntPtr sessionPtr, IntPtr trackPtr);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_player_seek(IntPtr sessionPtr, int offset);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_player_play(IntPtr sessionPtr, bool play);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_session_player_unload(IntPtr sessionPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_session_playlistcontainer(IntPtr sessionPtr);

        /* Search */
        [DllImport("libspotify")]
        public static extern ErrorCode sp_search_release(IntPtr searchPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_search_create(IntPtr sessionPtr, IntPtr query, int track_offset, int track_count,
                                                       int album_offset, int album_count, int artist_offset, int artist_count,
                                                       int playlist_offset, int playlist_count, SearchType search_type,
                                                       IntPtr callbackPtr, IntPtr userDataPtr);

        [DllImport("libspotify")]
        public static extern int sp_search_num_tracks(IntPtr searchPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_search_track(IntPtr searchPtr, int index);

        [DllImport("libspotify")]
        public static extern int sp_search_num_albums(IntPtr searchPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_search_album(IntPtr searchPtr, int index);

        [DllImport("libspotify")]
        public static extern int sp_search_num_artists(IntPtr searchPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_search_artist(IntPtr searchPtr, int index);

        [DllImport("libspotify")]
        public static extern ErrorCode sp_search_error(IntPtr searchPtr);

        /* Track */
        [DllImport("libspotify")]
        public static extern int sp_track_num_artists(IntPtr trackPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_track_artist(IntPtr trackPtr, int index);

        [DllImport("libspotify")]
        public static extern IntPtr sp_track_album(IntPtr trackPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_track_name(IntPtr trackPtr);

        [DllImport("libspotify")]
        public static extern int sp_track_duration(IntPtr trackPtr);

        [DllImport("libspotify")]
        public static extern int sp_track_index(IntPtr trackPtr);

        /* Artist */
        [DllImport("libspotify")]
        public static extern IntPtr sp_artist_name(IntPtr artistPtr);

        /* Playlist */
        [DllImport("libspotify")]
        public static extern int sp_playlistcontainer_num_playlists(IntPtr playlistContainerPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_playlistcontainer_playlist(IntPtr playlistContainerPtr, int index);

        [DllImport("libspotify")]
        public static extern int sp_playlist_num_tracks(IntPtr playlistPtr);

        [DllImport("libspotify")]
        public static extern IntPtr sp_playlist_track(IntPtr playlistPtr, int index);

        [DllImport("libspotify")]
        public static extern IntPtr sp_playlist_name(IntPtr playlistPtr);


        /* Error */
        [DllImport("libspotify")]
        public static extern IntPtr sp_error_message(ErrorCode error);
    }
}