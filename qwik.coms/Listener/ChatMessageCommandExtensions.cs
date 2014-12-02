using qwik.chatscan;
using qwik.helpers.Chatters;
using System.Collections.Generic;
using System.Linq;

namespace qwik.coms.Listener
{
    public static class ChatMessageCommandExtensions
    {
        public static string GetCommand(this ChatMessage chatMessage, string trigger)
        {
            var triggerLength = trigger.Length;
            var firstSpace = chatMessage.Message.Substring(triggerLength).IndexOf(' ');
            if (firstSpace != -1) firstSpace += triggerLength;
            return firstSpace == -1
                ? chatMessage.Message.Substring(triggerLength).ToLower()
                : chatMessage.Message.Substring(triggerLength, firstSpace - triggerLength).ToLower();
        }

        public static string GetArguments(this ChatMessage chatMessage, string command, string trigger)
        {
            var preArgsLength = command.Length + trigger.Length;
            if (preArgsLength < chatMessage.Message.Length) preArgsLength++;
            return chatMessage.Message.Substring(preArgsLength);
        }

        public static bool FromScreenNames(this ChatMessage chatMessage, IEnumerable<ScreenName> screenNames)
        {
            return screenNames.Any(x => chatMessage.Sender.Equals(x));
        }

        public static bool Trigger(this ChatMessage chatMessage, string trigger)
        {
            return chatMessage.Message.ToLower().StartsWith(trigger.ToLower());
        }
    }
}