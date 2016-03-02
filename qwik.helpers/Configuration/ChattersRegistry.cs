using qwik.helpers.Chatters;
using StructureMap;

namespace qwik.helpers.Configuration
{
    public class ChattersRegistry : Registry
    {
        public ChattersRegistry()
        {
            For<IChatterSearcher>().Use<ChatterSearcher>();
        }
    }
}