using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Music.Playlist
{
    public class GetPlaylistCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;

        public GetPlaylistCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "pl?" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var playlist = string.IsNullOrWhiteSpace(_settings.SpotifyPlaylist) ? "n/a" : _settings.SpotifyPlaylist;
            _output.Output($"Current playlist: {playlist}");
        }
    }
}