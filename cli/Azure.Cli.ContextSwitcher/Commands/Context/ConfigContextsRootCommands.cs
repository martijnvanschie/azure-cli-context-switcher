namespace Azure.Cli.ContextSwitcher.Commands.Contexts
{
    internal class ContextsRootCommands : CommandBase
    {
        public ContextsRootCommands(string? description = "Manage login context configurations") : base("context", description)
        {
            AddCommand(new DescribeContextsCommand("Prints the details about the context configuration."));
            AddCommand(new ListContextsCommand("List all the contexts which are avaialble in the configuration."));
            AddCommand(new SelectContextCommand("Switch context by selecting from a list of available contexts and perform a login."));
            AddCommand(new SetCurrentContextCommand("Set the current context in the configuration and perform a login."));
        }
    }
}
