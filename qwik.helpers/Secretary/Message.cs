using qwik.helpers.Chatters;

namespace qwik.helpers.Secretary
{
    public class Message
    {
        protected Message()
        {    
        }

        public Message(string sender, string body) 
            : this(sender.ToScreenName(), body)
        {
        }

        public Message(ScreenName sender, string body)
        {
            Sender = sender;
            Body = body;
        }

        public ScreenName Sender { get; set; }
        public string Body { get; set; }
    }
}