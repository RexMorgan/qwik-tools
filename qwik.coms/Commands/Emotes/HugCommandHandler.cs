using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Chatters;
using qwik.helpers.Settings;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Commands.Emotes
{
    public class HugCommandHandler : BaseCommandHandler
    {
        private readonly IChatterSearcher _chatterSearcher;
        private readonly IOutput _output;
        private readonly IAppSettings _settings;

        public HugCommandHandler(IChatterSearcher chatterSearcher, IOutput output, IAppSettings settings)
        {
            _chatterSearcher = chatterSearcher;
            _output = output;
            _settings = settings;
        }

        public override IEnumerable<string> Commands => new[] {"hug"};
        public override bool RequiresArguments => true;

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var chatters = _chatterSearcher.FindChatters(arguments).ToList();
            var numberOfChatters = chatters.Count;
            Hug(numberOfChatters == 1 ? chatters.Single().Formatted : arguments);
        }

        private void Hug(string chatter)
        {
            _output.Output($"[{_settings.Handle}] gives {chatter} a hug");
        }
    }
}