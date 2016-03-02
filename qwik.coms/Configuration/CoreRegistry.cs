using StructureMap;

namespace qwik.coms.Configuration
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<CoreRegistry>();
                s.IncludeNamespaceContainingType<CoreRegistry>();
                s.LookForRegistries();
                s.ExcludeType<CoreRegistry>();
            });

            IncludeRegistry<helpers.Configuration.CoreRegistry>();
            IncludeRegistry<spotify.Configuration.CoreRegistry>();
        }
    }
}