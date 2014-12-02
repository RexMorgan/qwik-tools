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

        public override IEnumerable<string> Commands
        {
            get { return new[] {"adv", "advertise"}; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _output.Formatted("qwik.coms¹ by [qwik]");
            _output.Formatted("at the command of [{0}]", _settings.Handle);
        }
    }
}