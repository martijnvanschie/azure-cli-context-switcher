namespace Azure.Cli.ContextSwitcher.Commands.Config.Users
{
    internal class ConfigUsersRootCommands : CommandBase
    {
        public ConfigUsersRootCommands(string? description = "Manage users in the configuration") : base("users", description)
        {
            AddCommand(new AddUserCommand("Add a user entry to the configuration file."));
            AddCommand(new AddUserPromptedCommand("Add a user entry to the configuration file using an interactive prompt."));
            AddCommand(new ListUserCommand("List all the users in the configuration."));
        }
    }
}
