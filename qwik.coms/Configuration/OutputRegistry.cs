using qwik.coms.Output;
using StructureMap;

namespace qwik.coms.Configuration
{
    public class OutputRegistry : Registry
    {
        public OutputRegistry()
        {
            For<IOutput>().Singleton().Use<ChatOutput>();
            For<IChatRateLimiter>().Singleton().Use<ChatRateLimiter>();
        }
    }
}