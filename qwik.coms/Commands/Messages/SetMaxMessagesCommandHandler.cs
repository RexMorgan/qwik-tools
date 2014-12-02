using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Messages
{
    public class SetMaxMessagesCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly IOutput _output;

        public SetMaxMessagesCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "maxm" }; }
        }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            int value;
            if (!int.TryParse(arguments, out value))
            {
                _output.Formatted("{0} is not a valid number", arguments);
                return;
            }
            _settings.MaxMessages = value;
            _settingsWriter.SaveSettings(_settings);
            _output.Formatted("Your max messages has been saved: {0}", _settings.MaxMessages);
        }
    }
}