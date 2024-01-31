using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Users
{
    internal class AddUserPromptedCommand : ConfigCommandBase
    {
        LoginType loginType;
        string keyName;
        string displayName;
        string? username;
        string? password;
        
        public AddUserPromptedCommand(string? description = null) : base("add-prompt", description)
        {
            this.SetHandler((configFile) =>
            {
                try
                {
                    loginType = AnsiConsole.Prompt(
                            new SelectionPrompt<LoginType>()
                                .Title("Select the login [green]type[/] you want to add...")
                                .PageSize(10)
                                .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                                .AddChoices(LoginType.Interactive, LoginType.ServicePrincipal, LoginType.UsernamePassword)
                                );

                    AnsiConsole.MarkupLine("A user entry requires a unique name used as the key to identity this user in the config.");
                    AnsiConsole.MarkupLine("[green]Allowed characters. Input will be formatted according.[/]");
                    AnsiConsole.MarkupLine("- Alphanumeric lowercase [yellow][[a-z]][/]");
                    AnsiConsole.MarkupLine("- Special characters [yellow]-_+[/]");
                    keyName = AnsiConsole.Ask<string>("What's the [green]unique name[/] for this user?");

                    AnsiConsole.MarkupLine("The display name is used as a descriptive. It is not used to for the login process.");
                    displayName = AnsiConsole.Ask<string>("What's the [green]display name[/] used for this entry.?");

                    switch (loginType)
                    {
                        case LoginType.Interactive:
                            break;
                        case LoginType.ServicePrincipal: //https://learn.microsoft.com/en-us/cli/azure/azure-cli-sp-tutorial-2
                            username = AnsiConsole.Ask<string>("Enter the [green]Application ID[/] for this service principal");
                            password = AnsiConsole.Ask<string>("Enter the [green]client secret[/] for this service principal");
                            break;
                        case LoginType.UsernamePassword:
                            username = AnsiConsole.Ask<string>("Enter the [green]username[/] for this login");
                            password = AnsiConsole.Prompt(
                                new TextPrompt<string>("Enter the [green]password[/] for this login")
                                    .PromptStyle("yellow")
                                    .Secret());
                            break;
                        default:
                            break;
                    }

                    var content = AzureCliContextManager.ReadConfig(configFile);
                    content.AddUser(keyName, loginType, friendlyName: displayName, username, password);

                    AzureCliContextManager.WriteConfig(content, configFile);

                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]User {keyName} was added to the configuration.[/]");
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