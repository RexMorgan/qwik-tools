using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using qwik.spotify;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Commands.Music.Playlist
{
    public class SetPlaylistCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly IOutput _output;
        private readonly IPlayer _player;

        public SetPlaylistCommandHandler(IAppSettings settings, IOutput output, IAppSettingsWriter settingsWriter, IPlayer player)
        {
            _settings = settings;
            _output = output;
            _settingsWriter = settingsWriter;
            _player = player;
        }

        public override IEnumerable<string> Commands => new[] { "pl" };

        public override bool RequiresArguments => true;

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var playlists = _player.Playlists().Where(x => x.Name.ToLower().Contains(arguments.ToLower())).ToList();
            if (!playlists.Any())
            {
                _output.Output($"No playlist found containing: {arguments}");
                return;
            }

            if (playlists.Count > 1)
            {
                _output.Output($"Multiple playlists found containing: {arguments}, be more specific.");
                for (var i = 0; i < playlists.Count; ++i)
                {
                    _output.Output($"{i+1}. {playlists[i].Name}");
                }
                return;
            }

            _settings.SpotifyPlaylist = playlists.Single().Name;
            _settingsWriter.SaveSettings(_settings);
            _output.Output($"Playlist set: {_settings.SpotifyPlaylist}");
        }
    }
}