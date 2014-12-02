using System.Collections.Generic;
using System.Linq;

namespace qwik.helpers.Chatters
{
    public class ChatterSearcher : IChatterSearcher
    {
        public IEnumerable<ScreenName> FindChatters(string partial)
        {
            return Chat.WhoIsChatting()
                .Where(x => Regexes.Spaces.Replace(x.ToLower(), string.Empty).Contains(partial))
                .Select(x => new ScreenName(x))
                .ToList();
        }
    }
}