using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Output.LeftAscii
{
    public class GetLeftAsciiCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IAppSettings _settings;

        public GetLeftAsciiCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "la?" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _output.Output($"Left ascii is: {_settings.LeftAscii}");
        }
    }
}