using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Output.LeftAscii
{
    public class LeftAsciiFontCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;

        public LeftAsciiFontCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] {"lafont"}; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.LeftAsciiFont = arguments;
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("Left ascii font updated to: {0}", _settings.LeftAsciiFont);
        }
    }
}