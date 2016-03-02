using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands
{
    public class AdvertiseCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IAppSettings _settings;

        public AdvertiseCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] {"adv", "advertise"};

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _output.Output("qwik.coms¹ by [qwik]");
            _output.Output($"at the command of [{_settings.Handle}]");
        }
    }
}