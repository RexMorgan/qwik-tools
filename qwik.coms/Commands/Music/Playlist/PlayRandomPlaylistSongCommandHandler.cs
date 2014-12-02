using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers;
using qwik.helpers.Settings;
using qwik.spotify;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Commands.Music.Playlist
{
    public class PlayRandomPlaylistSongCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;
        private readonly IPlayer _player;

        public PlayRandomPlaylistSongCommandHandler(IAppSettings settings, IPlayer player, IOutput output)
        {
            _settings = settings;
            _player = player;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "plr" }; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var playlist = _player.Playlists().SingleOrDefault(x => x.Name == _settings.SpotifyPlaylist);
            if (playlist == null)
            {
                _output.Formatted("Unable to find saved playlist: {0}, use command pl", _settings.SpotifyPlaylist);
                return;
            }

            var playlistTracks = playlist.Tracks.ToList();
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                playlistTracks = playlistTracks.Where(x => x.Artist.ToLower().Contains(arguments) || x.Name.ToLower().Contains(arguments)).ToList();
            }
            var random = Random.Rand(0, playlistTracks.Count);
            var randomTrack = playlistTracks[random];
            _player.Play(randomTrack.TrackPtr);
            _output.Formatted("Playing {1} by {0} [{2:mm\\:ss}]", randomTrack.Artist, randomTrack.Name, randomTrack.Duration);
        }
    }
}