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

        public override IEnumerable<string> Commands
        {
            get { return new[] {"la"}; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.LeftAscii = arguments;
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("Left ascii changed to: {0}", _settings.LeftAscii);
        }
    }
}