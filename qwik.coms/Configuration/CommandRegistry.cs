using qwik.coms.Commands;
using qwik.coms.Listener;
using StructureMap;

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
            For<ICommandExecutor>().Singleton().Use<CommandExecutor>();
        }
    }
}