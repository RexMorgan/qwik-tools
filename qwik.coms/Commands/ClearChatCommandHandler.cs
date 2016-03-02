using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers;
using System.Collections.Generic;

namespace qwik.coms.Commands
{
    public class ClearChatCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;

        public ClearChatCommandHandler(IOutput output)
        {
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "clr", "clear" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            // TODO: This freezes AOL and makes me have to restart it.
            Chat.Clear();
            _output.Output("Chat has been cleared");
        }
    }
}