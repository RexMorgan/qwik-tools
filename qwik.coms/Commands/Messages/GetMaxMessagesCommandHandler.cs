using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Messages
{
    public class GetMaxMessagesCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;

        public GetMaxMessagesCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "maxm?" }; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _output.Formatted("Max messages is: {0}", _settings.MaxMessages);
        }
    }
}