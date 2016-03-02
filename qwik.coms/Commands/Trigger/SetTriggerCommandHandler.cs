using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Trigger
{
    public class SetTriggerCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly IOutput _output;

        public SetTriggerCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "trig", "trigger" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var trigger = arguments;
            if (string.IsNullOrEmpty(trigger)) trigger = string.Empty;

            _settings.Trigger = trigger;
            _settingsWriter.SaveSettings(_settings);

            var triggerOutput = string.IsNullOrEmpty(trigger) ? "n/a" : trigger;
            _output.Output($"New trigger saved: {triggerOutput}");
        }
    }
}