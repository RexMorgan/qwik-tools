using qwik.coms.Commands;
using qwik.coms.Listener;
using StructureMap.Configuration.DSL;

namespace qwik.coms.Configuration
{
    public class CommandRegistry : Registry
    {
        public CommandRegistry()
        {
            Scan(s =>
            {
                s.AssemblyContainingType<ICommandHandler>();
                s.IncludeNamespaceContainingType<ICommandHandler>();
                s.AddAllTypesOf<ICommandHandler>();
            });

            For<ICommandListener>().Singleton().Use<CommandListener>();
        }
    }
}