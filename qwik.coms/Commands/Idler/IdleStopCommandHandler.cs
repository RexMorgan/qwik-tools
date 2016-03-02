using qwik.chatscan;
using qwik.coms.idler;
using System.Collections.Generic;

namespace qwik.coms.Commands.Idler
{
    public class IdleStopCommandHandler : BaseCommandHandler
    {
        private readonly IIdler _idler;

        public IdleStopCommandHandler(IIdler idler)
        {
            _idler = idler;
        }

        public override IEnumerable<string> Commands => new[] {"back", "idleoff"};

        public override void Execute(string arguments, string command, ChatMessage chatMessage)
        {
            _idler.Stop();
        }
    }
}