using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Messages
{
    public class RemoveAllMessagesCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly IOutput _output;

        public RemoveAllMessagesCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
        }

        public override IEnumerable<string> Commands => new[] { "rmmsgs", "delmsgs" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var messageCount = _settings.Messages.Count;
            _settings.Messages.Clear();
            _settingsWriter.SaveSettings(_settings);
            _output.Output($"{messageCount} messages have been deleted");
        }
    }
}