using qwik.coms.Output;
using StructureMap.Configuration.DSL;

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