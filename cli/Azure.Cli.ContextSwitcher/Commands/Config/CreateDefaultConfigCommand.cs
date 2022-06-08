using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config
{
    internal class CreateDefaultConfigCommand : ConfigCommandBase
    {
        public CreateDefaultConfigCommand(string? description = null) : base("create", description)
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
                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Writing file[/]");
                    AzureCliContextManager.WriteConfig(AzureCliContext.CreateDefault(), configFile);
                    return;
                }

                AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Skipping file file[/]");
            },
            _configFile);
        }
    }
}
