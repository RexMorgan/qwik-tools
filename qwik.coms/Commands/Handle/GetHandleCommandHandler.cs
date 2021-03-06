﻿using qwik.chatscan;
using qwik.coms.Output;
using qwik.helpers.Settings;
using System.Collections.Generic;

namespace qwik.coms.Commands.Handle
{
    public class GetHandleCommandHandler : BaseCommandHandler
    {
        private readonly IAppSettings _appSettings;
        private readonly IOutput _output;

        public GetHandleCommandHandler(IOutput output, IAppSettings appSettings)
        {
            _output = output;
            _appSettings = appSettings;
        }

        public override IEnumerable<string> Commands => new[] { "handle?", "user?" };

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _output.Output($"Your handle is [{_appSettings.Handle}]");
        }
    }
}