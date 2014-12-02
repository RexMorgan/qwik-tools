using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Output.LeftAscii
{
    public class LeftAsciiItalicCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;

        public LeftAsciiItalicCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] {"laitalic"}; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.LeftAsciiItalic = !_settings.LeftAsciiItalic;
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("Left ascii italic: {0}", _settings.LeftAsciiItalic);
        }
    }
}