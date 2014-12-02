using qwik.chatscan;
using System.Collections.Generic;

namespace qwik.coms.Commands
{
    public interface ICommandHandler
    {
        bool RequiresArguments { get; }
        IEnumerable<string> Commands { get; }
        void Execute(string arguments, string command, ChatMessage chatMessage);
    }
}