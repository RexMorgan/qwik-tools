using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Output.LeftAscii
{
    public class LeftAsciiBoldCommandHandler : BaseCommandHandler
    {
        private readonly IOutput _output;
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;

        public LeftAsciiBoldCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] {"labold"}; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.LeftAsciiBold = !_settings.LeftAsciiBold;
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("Left ascii bold: {0}", _settings.LeftAsciiBold);
        }
    }
}