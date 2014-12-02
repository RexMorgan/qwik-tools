using System.Collections.Generic;
using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;

namespace qwik.coms.Commands.Trigger
{
    public class GetTriggerCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;

        public GetTriggerCommandHandler(IAppSettings settings, IOutput output)
        {
            _settings = settings;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "trig?", "trigger?" }; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var triggerOutput = string.IsNullOrEmpty(_settings.Trigger) ? "n/a" : _settings.Trigger;
            _output.Formatted("Current trigger is: {0}", triggerOutput);
        }
    }
}