using qwik.helpers.Chatters;

namespace qwik.chatscan
{
    public class ChatMessage
    {
        public ChatMessage(ScreenName sender, string message)
        {
            Sender = sender;
            Message = message;
        }

        public ChatMessage(string sender, string message)
        {
            Sender = new ScreenName(sender);
            Message = message;
        }

        public ScreenName Sender { get; private set; }
        public string Message { get; private set; }
    }
}