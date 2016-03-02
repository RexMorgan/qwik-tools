using qwik.chatscan;
using qwik.coms.Output;
using qwik.coms.Secretary;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Secretary
{
    public class SecretaryToggleCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _settings;
        private readonly IAppSettingsWriter _settingsWriter;
        private readonly IOutput _output;
        private readonly ISecretary _secretary;

        public SecretaryToggleCommandHandler(IAppSettings settings, IAppSettingsWriter settingsWriter, IOutput output, ISecretary secretary)
        {
            _settings = settings;
            _settingsWriter = settingsWriter;
            _output = output;
            _secretary = secretary;
        }

        public override IEnumerable<string> Commands => new[] { "secretary", "sec" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _settings.Secretary = !_settings.Secretary;
            _settingsWriter.SaveSettings(_settings);
            var onOrOff = _settings.Secretary ? "on" : "off";
            _output.Output($"Secretary is now: {onOrOff}");
            if (_settings.Secretary)
            {
                _secretary.Start();
            }
            else
            {
                _secretary.Stop();
            }
        }
    }
}