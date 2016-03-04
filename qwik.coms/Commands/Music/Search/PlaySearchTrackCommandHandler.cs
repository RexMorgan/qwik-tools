using System.Collections.Generic;
using qwik.chatscan;
using qwik.coms.spotify;

namespace qwik.coms.Commands.Music.Search
{
    public class PlaySearchTrackCommandHandler : BaseCommandHandler
    {
        private readonly IPlayer _player;

        public PlaySearchTrackCommandHandler(IPlayer player)
        {
            _player = player;
        }

        public override IEnumerable<string> Commands => new[] { "st" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var searchQuery = arguments.Trim();
        }
    }
}