﻿using qwik.chatscan;
using System.Collections.Generic;
using qwik.coms.spotify.Sessions;

namespace qwik.coms.Commands
{
    public class ExitCommandHandler : BaseCommandHandler
    {
        private readonly ISession _session;

        public ExitCommandHandler(ISession session)
        {
            _session = session;
        }

        public override IEnumerable<string> Commands => new[] {"exit", "quit"};

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _session.Logout();
        }
    }
}