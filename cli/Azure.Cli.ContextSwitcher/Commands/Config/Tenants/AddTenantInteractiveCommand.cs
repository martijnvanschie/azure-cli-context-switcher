using Azure.Cli.ContextSwitcher.Model;
using Spectre.Console;
using System.CommandLine;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Tenants
{
    internal class AddTenantInteractiveCommand : ConfigCommandBase
    {
        public AddTenantInteractiveCommand(string? description = null) : base("add-prompt", description)
        {
            this.SetHandler((configFile) =>
            {
                try
                {
                    AnsiConsole.MarkupLine("A tenant entry requires a unique name used as the key to identity this tenant in the config.");
                    AnsiConsole.MarkupLine("[green]Allowed characters. Input will be formatted according.[/]");
                    AnsiConsole.MarkupLine("- Alphanumeric lowercase [yellow][[a-z]][/]");
                    AnsiConsole.MarkupLine("- Special characters [yellow]-_+[/]");
                    var uniqueName = AnsiConsole.Ask<string>("What's the [green]name[/] for this tenant?");

                    AnsiConsole.WriteLine();
                    var friendlyName = AnsiConsole.Ask<string>("Enter the [green]directory name[/] for this tenant.");

                    AnsiConsole.WriteLine();
                    AnsiConsole.MarkupLine("The tenant id is used to login a service principal. You can either enter the [yellow]id[/] or the [yellow]domain name[/] of the tenant.");
                    var tenantId = AnsiConsole.Ask<string>("Enter the [green]tenant id[/] for this tenant.");

                    var config = AzureCliContextManager.ReadConfig(configFile);

                    config.AddTenant(uniqueName, tenantId, friendlyName);

                    AzureCliContextManager.WriteConfig(config, configFile);

                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Tentant {uniqueName} was added to the config.[/]");
                }
                catch (ContextConfigurationException ex)
                {
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]{ex.Message.EscapeMarkup()}[/]");
                }
            },
            _configFile);
        }
    }
}
