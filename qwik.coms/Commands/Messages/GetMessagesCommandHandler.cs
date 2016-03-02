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

        public override IEnumerable<string> Commands => new[] { "msgs" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.Messages.Each(message => _output.Output($"{message.Sender.LowerCased}: {message.Body}"));
        }
    }
}