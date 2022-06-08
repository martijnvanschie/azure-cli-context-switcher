using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config
{
    internal class ViewConfigCommand : ConfigCommandBase
    {
        public ViewConfigCommand(string? description = null) : base("view", description)
        {
            this.SetHandler((string test) =>
            {
                AnsiConsole.MarkupLine($"Using context name [lightgoldenrod2_1]{test}[/]");

                var content = AzureCliContextManager.ReadConfigAsString(test);
                AnsiConsole.MarkupLine($"[lightgoldenrod2_1]{content}[/]");
            },
            _configFile);
        }
    }
}
