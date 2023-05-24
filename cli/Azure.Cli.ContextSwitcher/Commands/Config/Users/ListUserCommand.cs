using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Users
{
    internal class ListUserCommand : ConfigCommandBase
    {
        public ListUserCommand(string? description = null) : base("list", description)
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
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Login Type[/]");
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Username[/]");

                    foreach (var item in content.Users)
                    {
                        table.AddRow(item.Key, item.Value.DisplayName, item.Value.LoginType.ToString(), item.Value.Username ?? $"[{Color.Grey}]n.a.[/]");
                    }

                    // Render the table to the console
                    AnsiConsole.Write(table);
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
