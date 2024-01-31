using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config
{
    internal class ViewConfigCommand : ConfigCommandBase
    {
        public ViewConfigCommand(string? description = null) : base("view", description)
        {
            this.SetHandler((string configName) =>
            {
                AnsiConsole.MarkupLine($"Context: [lightgoldenrod2_1]{configName}[/]");

                var path = AzureCliContextManager.GetConfigFile(configName);
                AnsiConsole.MarkupLine($"Path: [lightgoldenrod2_1]{path}[/]");

                var content = AzureCliContextManager.ReadConfigAsString(configName);
                AnsiConsole.MarkupLineInterpolated($"[lightgoldenrod2_1]{content}[/]");
            },
            _configFile);
        }
    }
}
