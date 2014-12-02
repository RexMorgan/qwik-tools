using qwik.coms.idler;
using StructureMap.Configuration.DSL;

namespace qwik.coms.Configuration
{
    public class IdlerRegistry : Registry
    {
        public IdlerRegistry()
        {
            For<IIdler>().Singleton().Use<Idler>();
            For<IIdlerFormatter>().Use<IdlerFormatter>();
        }
    }
}