using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Chatters;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Commands.Emotes
{
    public class LowerCaseCommandHandler : BaseCommandHandler
    {
        private readonly IChatterSearcher _chatterSearcher;
        private readonly IOutput _output;

        public LowerCaseCommandHandler(IChatterSearcher chatterSearcher, IOutput output)
        {
            _chatterSearcher = chatterSearcher;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] {"lcase"}; }
        }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var chatters = _chatterSearcher.FindChatters(arguments).ToList();
            if (chatters.Any())
            {
                _output.Formatted("Lower cased: {0}", string.Join(", ", chatters.Select(x => x.Readable)));
            }
        }
    }
}