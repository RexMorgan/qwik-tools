using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Messages
{
    public class GetMessagesCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;

        public GetMessagesCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "msgs" }; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            foreach (var message in _settings.Messages)
            {
                _output.Formatted("{0}: {1}", message.Sender.LowerCased, message.Body);
            }
        }
    }
}