using qwik.coms.Secretary;
using StructureMap.Configuration.DSL;

namespace qwik.coms.Configuration
{
    public class SecretaryRegistry : Registry
    {
        public SecretaryRegistry()
        {
            For<ISecretary>().Singleton().Use<Secretary.Secretary>();
        }
    }
}