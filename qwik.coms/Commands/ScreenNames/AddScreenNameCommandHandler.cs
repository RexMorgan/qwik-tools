﻿using qwik.chatscan;
using qwik.coms.Output;
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

        public override IEnumerable<string> Commands => new[] { "addsn" };

        public override bool RequiresArguments => true;

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var screenname = new ScreenName(arguments);
            if (_settings.ScreenNames.Contains(screenname))
            {
                _output.Output($"ScreenName is already added: {screenname.Readable}");
                return;
            }

            _settings.ScreenNames.Fill(screenname);
            _settingsWriter.SaveSettings(_settings);
            _output.Output($"ScreenName has been added: {screenname.Readable}");
        }
    }
}