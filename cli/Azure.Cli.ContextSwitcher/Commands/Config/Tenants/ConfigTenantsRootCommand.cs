using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Tenants
{
    internal class ConfigTenantsRootCommands : CommandBase
    {
        public ConfigTenantsRootCommands(string? description = "Manage users in the configuration") : base("tenants", description)
        {
            AddCommand(new AddTenantCommand("Add a new tenant to the configuration."));
            AddCommand(new AddTenantInteractiveCommand("Add a new tenant to the configuration using an interactive prompt."));
            AddCommand(new ListTenantsCommand("List all the tenants in the configuration."));
        }
    }
}
