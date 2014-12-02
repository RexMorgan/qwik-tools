﻿using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Handle
{
    public class SetHandleCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _appSettings;
        private readonly IAppSettingsWriter _appSettingsWriter;
        private readonly IOutput _output;

        public SetHandleCommandHandler(IAppSettings appSettings, IOutput output, IAppSettingsWriter appSettingsWriter)
        {
            _appSettings = appSettings;
            _output = output;
            _appSettingsWriter = appSettingsWriter;
        }

        public override bool RequiresArguments
        {
            get { return true; }
        }

        public override IEnumerable<string> Commands
        {
            get { return new[] { "handle", "user" }; }
        }

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            var handle = arguments;
            _appSettings.Handle = handle;
            _appSettingsWriter.SaveSettings(_appSettings);
            _output.Formatted("Your handle is now [{0}]", handle);
        }
    }
}