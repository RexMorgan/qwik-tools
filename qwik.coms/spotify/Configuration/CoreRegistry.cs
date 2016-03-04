using StructureMap;

namespace qwik.coms.spotify.Configuration
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
        }
    }
}