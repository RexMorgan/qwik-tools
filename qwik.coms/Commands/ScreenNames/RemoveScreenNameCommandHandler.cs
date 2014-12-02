using System.Collections.Generic;
using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers;
using qwik.helpers.Chatters;
using qwik.helpers.Settings;

namespace qwik.coms.Commands.ScreenNames
{
    public class RemoveScreenNameCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IOutput _output;
        private readonly IAppSettingsWriter _settingsWriter;

        public RemoveScreenNameCommandHandler(IAppSettings settings, IOutput output, IAppSettingsWriter settingsWriter)
        {
            _settings = settings;
            _output = output;
            _settingsWriter = settingsWriter;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "rmsn", "remsn", "delsn" }; }
        }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var screenname = new ScreenName(arguments);
            if (!_settings.ScreenNames.Contains(screenname))
            {
                _output.Formatted("ScreenName not found: {0}", screenname.Readable);
                return;
            }

            _settings.ScreenNames.Remove(screenname);
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("ScreenName has been removed: {0}", screenname.Readable);
        }
    }
}