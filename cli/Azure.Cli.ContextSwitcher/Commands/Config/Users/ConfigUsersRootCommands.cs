namespace Azure.Cli.ContextSwitcher.Commands.Config.Users
{
    internal class ConfigUsersRootCommands : CommandBase
    {
        public ConfigUsersRootCommands(string? description = "Manage users in the configuration") : base("users", description)
        {
            AddCommand(new ListUserCommand("List all the users in the configuration."));
            AddCommand(new AddUserCommand("Add a user entry to the configuration file."));
        }
    }
}
