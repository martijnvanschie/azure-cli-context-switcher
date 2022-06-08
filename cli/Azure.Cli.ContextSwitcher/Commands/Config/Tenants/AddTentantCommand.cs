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
            var name = new Option<string>("--name", "Define the type of login this user represents.");
            name.AddAlias("-n");
            name.IsRequired = true;
            AddOption(name);

            var friendlyName = new Option<string>("--friendly-name", "Define the type of login this user represents.");
            friendlyName.AddAlias("-fn");
            friendlyName.IsRequired = false;
            AddOption(friendlyName);

            var tenantId = new Option<string>("--tenant-id", "Define the type of login this user represents.");
            tenantId.IsRequired = true;
            tenantId.AddAlias("-t");
            AddOption(tenantId);

            this.SetHandler((configFile, name, tenantId, friendlyName) =>
            {
                try
                {
                    var config = AzureCliContextManager.ReadConfig(configFile);

                    config.AddTenant(name, tenantId, friendlyName);

                    AzureCliContextManager.WriteConfig(config);

                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]User {name} was added.[/]");
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
