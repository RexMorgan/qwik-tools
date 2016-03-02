using qwik.chatscan;
using qwik.coms.Output;
using qwik.spotify;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Commands.Music.Playlist
{
    public class PlayRandomPlaylistSongCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IPlayer _player;

        public PlayRandomPlaylistSongCommandHandler(IPlayer player, IOutput output)
        {
            _player = player;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "plr" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var playlist = _player.CurrentPlaylist;
            if (playlist == null)
            {
                _output.Output("No playlist set, to set one, use the command pl");
                return;
            }

            var playlistTracks = playlist.Tracks.ToArray();
            if (!string.IsNullOrWhiteSpace(arguments))
            {
                playlistTracks = playlistTracks.Where(x => x.Artist.ToLower().Contains(arguments) || x.Name.ToLower().Contains(arguments)).ToArray();
                if (playlistTracks.Length == 0)
                {
                    playlistTracks = playlist.Tracks.ToArray();
                    _output.Output($"No matches in playlist for: {arguments}");
                }
            }
            var randomTrack = playlistTracks.Random();
            _player.Play(randomTrack.TrackPtr);
        }
    }
}