using System.Collections.Generic;

namespace qwik.helpers.Chatters
{
    public interface IChatterSearcher
    {
        IEnumerable<ScreenName> FindChatters(string partial);
    }
}