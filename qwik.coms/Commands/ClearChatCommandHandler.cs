using System.Collections.Generic;
using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers;

namespace qwik.coms.Commands
{
    public class ClearChatCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;

        public ClearChatCommandHandler(IOutput output)
        {
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "clr", "clear" }; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            Chat.Clear();
            _output.Formatted("Chat has been cleared");
        }
    }
}