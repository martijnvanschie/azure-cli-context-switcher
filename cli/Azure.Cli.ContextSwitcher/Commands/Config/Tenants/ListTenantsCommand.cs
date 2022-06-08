using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Tenants
{
    internal class ListTenantsCommand : ConfigCommandBase
    {
        public ListTenantsCommand(string? description = null) : base("list", description)
        {
            this.SetHandler((configFile) =>
            {
                try
                {
                    var content = AzureCliContextManager.ReadConfig(configFile);

                    // Create a table
                    var table = new Table();
                    table.Border = TableBorder.Ascii2;
                    table.Title = new TableTitle($"[{Color.Aqua}]Users for config [[{configFile}]][/]");

                    // Add some columns
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Name[/]");
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Friendly Name[/]");
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Tenant Id[/]");

                    foreach (var item in content.Tenants)
                    {
                        table.AddRow(item.Key, item.Value.Name, item.Value.TenantId ?? $"[{Color.Grey}]n.a.[/]");
                    }

                    // Render the table to the console
                    AnsiConsole.Write(table);
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]{ex.Message.EscapeMarkup()}[/]");
                }
            },
            _configFile);
        }
    }
}
