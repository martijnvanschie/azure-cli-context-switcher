using Azure.Cli.ContextSwitcher.Commands.Config.Contexts;
using Azure.Cli.ContextSwitcher.Commands.Config.Tenants;
using Azure.Cli.ContextSwitcher.Commands.Config.Users;

namespace Azure.Cli.ContextSwitcher.Commands.Config
{
    internal class ConfigRootCommands : CommandBase
    {
        public ConfigRootCommands(string? description = "Manage resource groups") : base("config", description)
        {
            AddCommand(new ViewConfigCommand("View the current configuration"));
            AddCommand(new CreateDefaultConfigCommand("Create a default cli config file."));

            AddCommand(new ConfigTenantsRootCommands());
            AddCommand(new ConfigUsersRootCommands());
        }
    }
}
