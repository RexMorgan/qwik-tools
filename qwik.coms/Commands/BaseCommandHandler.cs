using qwik.chatscan;
using System.Collections.Generic;

namespace qwik.coms.Commands
{
    public abstract class BaseCommandHandler : ICommandHandler
    {
        public virtual bool RequiresArguments => false;
        public abstract IEnumerable<string> Commands { get; }
        public abstract void Execute(string arguments, string command, ChatMessage chatMessage);
    }
}