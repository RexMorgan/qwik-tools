using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers;
using qwik.helpers.Chatters;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.ScreenNames
{
    public class AddScreenNameCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;
        private readonly IAppSettingsWriter _settingsWriter;

        public AddScreenNameCommandHandler(IAppSettings settings, IOutput output, IAppSettingsWriter settingsWriter)
        {
            _settings = settings;
            _output = output;
            _settingsWriter = settingsWriter;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "addsn" }; }
        }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var screenname = new ScreenName(arguments);
            if (_settings.ScreenNames.Contains(screenname))
            {
                _output.Formatted("ScreenName is already added: {0}", screenname.Readable);
                return;
            }

            _settings.ScreenNames.Fill(screenname);
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("ScreenName has been added: {0}", screenname.Readable);
        }
    }
}