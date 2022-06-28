using Azure.Cli.ContextSwitcher.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.CommandLine;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Tenants
{
    internal class AddTenantCommand : ConfigCommandBase
    {
        public AddTenantCommand(string? description = null) : base("add", description)
        {
            var name = new Option<string>("--name", "The unique name of the tenant. Used to map tenants to users in the context config.");
            name.AddAlias("-n");
            name.IsRequired = true;
            AddOption(name);

            var tenantId = new Option<string>("--tenant-id", "The tenant id used when performing the login.");
            tenantId.IsRequired = true;
            tenantId.AddAlias("-t");
            AddOption(tenantId);

            var friendlyName = new Option<string>("--friendly-name", "The friendly name of the tenant. Used as a descriptive name. If not set the --name option is used.");
            friendlyName.AddAlias("-fn");
            friendlyName.IsRequired = false;
            AddOption(friendlyName);

            this.SetHandler((configFile, name, tenantId, friendlyName) =>
            {
                try
                {
                    var config = AzureCliContextManager.ReadConfig(configFile);

                    config.AddTenant(name, tenantId, friendlyName);

                    AzureCliContextManager.WriteConfig(config);

                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Tentant {name} was added to the config.[/]");
                }
                catch (ContextConfigurationException ex)
                {
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]{ex.Message.EscapeMarkup()}[/]");
                }
            },
            _configFile, name, tenantId, friendlyName);
        }
    }
}
