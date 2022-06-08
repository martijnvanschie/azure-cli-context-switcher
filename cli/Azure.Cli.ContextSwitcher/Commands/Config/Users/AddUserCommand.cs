using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Users
{
    internal class AddUserCommand : ConfigCommandBase
    {
        public AddUserCommand(string? description = null) : base("add", description)
        {
            var name = new Option<string>("--name", "Define the type of login this user represents.");
            name.AddAlias("-n");
            name.IsRequired = true;
            AddOption(name);

            var friendlyName = new Option<string>("--friendly-name", "Define the type of login this user represents.");
            friendlyName.AddAlias("-fn");
            friendlyName.IsRequired = false;
            AddOption(friendlyName);

            var userType = new Option<LoginType>("--type", () => LoginType.Interactive, "Define the type of login this user represents.");
            userType.AddAlias("-t");
            AddOption(userType);

            this.SetHandler((configFile, entryName, userType, friendlyName) =>
            {
                try
                {
                    var content = AzureCliContextManager.ReadConfig(configFile);

                    content.AddUser(entryName, friendlyName: friendlyName);

                    AzureCliContextManager.WriteConfig(content);

                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]User {entryName} was added.[/]");
                }
                catch (ContextConfigurationException ex)
                {
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]{ex.Message.EscapeMarkup()}[/]");
                }
            },
            _configFile, name, userType, friendlyName);
        }
    }
}
