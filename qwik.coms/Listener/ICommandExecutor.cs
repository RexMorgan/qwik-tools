using qwik.chatscan;

namespace qwik.coms.Listener
{
    public interface ICommandExecutor
    {
        void Execute(ChatMessage message);
    }
}