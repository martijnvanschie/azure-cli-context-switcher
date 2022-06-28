﻿namespace Azure.Cli.ContextSwitcher.Commands.Config.Contexts
{
    internal class ConfigContextsRootCommands : CommandBase
    {
        public ConfigContextsRootCommands(string? description = "Manage users in the configuration") : base("contexts", description)
        {
            AddCommand(new ListContextsCommand("List all the contexts in the configuration."));
            AddCommand(new SetCurrentContextCommand("Sets the current context in the configuration and performs a login."));
            AddCommand(new DescribeContextsCommand("Prints the details about the context configuration"));
            AddCommand(new SelectContextCommand("Switch context by selecting from a list of available contexts."));
        }
    }
}
