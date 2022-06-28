using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure.Cli.ContextSwitcher.Commands.Config;
using Azure.Cli.ContextSwitcher.Commands.Config.Contexts;

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
            RootCommand.AddCommand(new ConfigContextsRootCommands());
        }

        internal void Invoke(string[] args)
        {
            RootCommand.Invoke(args);
        }
    }
}
