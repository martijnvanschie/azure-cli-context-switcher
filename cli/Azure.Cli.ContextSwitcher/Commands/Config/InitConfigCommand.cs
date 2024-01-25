using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config
{
    internal class InitConfigCommand : ConfigCommandBase
    {
        public InitConfigCommand(string? description = null) : base("init", description)
        {
            this.SetHandler((string configFile) =>
            {
                var writeFile = true;

                var content = AzureCliContextManager.GetConfigFile(configFile);

                if (content.Exists)
                {
                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Config file [[{content.FullName}]] exists and will be overwritten.[/]");
                    writeFile = AnsiConsole.Confirm("Are you sure?");
                }

                if (writeFile)
                {
                    AzureCliContextManager.WriteConfig(AzureCliContext.CreateDefault(), configFile);
                    AnsiConsole.MarkupLine($"[green]Config file {content.FullName} initialized.[/]");
                    return;
                }

                AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Skipping init of file {content.FullName}[/]");
            },
            _configFile);
        }
    }
}
