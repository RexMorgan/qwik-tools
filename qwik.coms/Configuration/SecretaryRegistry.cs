using qwik.coms.Secretary;
using StructureMap;

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