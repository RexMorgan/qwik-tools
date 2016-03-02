using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

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

        public override IEnumerable<string> Commands => new[] { "trig?", "trigger?" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var triggerOutput = string.IsNullOrEmpty(_settings.Trigger) ? "n/a" : _settings.Trigger;
            _output.Output($"Current trigger is: {triggerOutput}");
        }
    }
}