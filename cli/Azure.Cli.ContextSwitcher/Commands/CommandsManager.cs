using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Cli.ContextSwitcher.Commands
{
    internal class CommandsManager
    {
        internal RootCommand RootCommand { get; private set; }

        internal CommandsManager()
        {
            RootCommand = new RootCommand();
            RootCommand.Description = "Azure Context Switcher v0.1";

            RootCommand.AddCommand(new Config.ConfigRootCommands());
        }

        internal void Invoke(string[] args)
        {
            RootCommand.Invoke(args);
        }
    }
}
