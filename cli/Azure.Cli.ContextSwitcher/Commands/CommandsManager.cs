using System.CommandLine;
using Azure.Cli.ContextSwitcher.Commands.Config;
using Azure.Cli.ContextSwitcher.Commands.Contexts;

namespace Azure.Cli.ContextSwitcher.Commands
{
    internal class CommandsManager
    {
        internal RootCommand RootCommand { get; private set; }

        internal CommandsManager()
        {
            RootCommand = new RootCommand();
            RootCommand.Description = "Azure Context Switcher v0.1";

            RootCommand.AddCommand(new ConfigRootCommands());
            RootCommand.AddCommand(new ContextsRootCommands());
        }

        internal void Invoke(string[] args)
        {
            RootCommand.Invoke(args);
        }
    }
}
