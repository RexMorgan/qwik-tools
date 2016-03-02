using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Output.LeftAscii
{
    public class SetLeftAsciiCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;

        public SetLeftAsciiCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] {"la"};

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.LeftAscii = arguments;
            _settingsWriter.SaveSettings(_settings);
            _output.Output($"Left ascii changed to: {_settings.LeftAscii}");
        }
    }
}