using qwik.chatscan;
using qwik.coms.idler;
using System.Collections.Generic;

namespace qwik.coms.Commands.Idler
{
    public class IdleStartCommandHandler : BaseCommandHandler
    {
        private readonly IIdler _idler;

        public IdleStartCommandHandler(IIdler idler)
        {
            _idler = idler;
        }

        public override IEnumerable<string> Commands => new[] {"idle", "idleon", "afk", "brb"};

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            if (string.IsNullOrWhiteSpace(arguments))
            {
                _idler.Start();
            }
            else
            {
                _idler.Start(arguments);
            }
        }
    }
}