using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;
using Azure.Cli.ContextSwitcher.Helpers;
using Azure.Cli.ContextSwitcher.Commands.Config;

namespace Azure.Cli.ContextSwitcher.Commands.Contexts
{
    internal class SelectContextCommand : ConfigCommandBase
    {
        const string CHOICE_CANCEL = "(cancel)";
        private AzureCliContext? _config;

        public SelectContextCommand(string? description = null) : base("select", description)
        {
            this.SetHandler((configFile) =>
            {
                _config = AzureCliContextManager.ReadConfig(configFile);

                var selection = new SelectionPrompt<string>()
                        .Title("Select the context you want to login to...")
                        .PageSize(10)
                        .MoreChoicesText("[grey](Move up and down to reveal more fruits)[/]")
                        .AddChoices(_config.Contexts.Select(c => c.Key).ToArray())
                        .AddChoices(CHOICE_CANCEL);

                var context = AnsiConsole.Prompt(selection);

                AnsiConsole.WriteLine($"Using context [{context}]");

                if (context.Equals(CHOICE_CANCEL))
                {
                    return;
                }

                var currentContext = _config.GetContext(context);

                if (currentContext is null)
                {
                    AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Context [[{context}]] was not found in config.[/]");
                    return;
                }

                try
                {
                    PerformLogin(currentContext);
                    _config.CurrentContext = context;
                    AzureCliContextManager.WriteConfig(_config, configFile);
                    AnsiConsole.MarkupLine($"[{Color.Lime}]Context successfully set.[/]");
                }
                catch (Exception ex)
                {
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]Failed to set current context.[/]");
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]{ex.Message.EscapeMarkup()}[/]");
                }
            },
            _configFile);
        }

        private void PerformLogin(Context currentContext)
        {
            var tenant = _config.Tenants[currentContext.Tenant];
            var user = _config.Users[currentContext.User];

            switch (user.LoginType)
            {
                case LoginType.Interactive:
                    Utilities.LoginInteractive(tenant);
                    break;
                case LoginType.ServicePrincipal:
                    Utilities.LoginServicePrincipal(tenant, user);
                    break;
                case LoginType.UsernamePassword:
                    Utilities.LoginUsernamePassword(tenant, user);
                    break;
                default:
                    break;
            }
        }
    }
}
