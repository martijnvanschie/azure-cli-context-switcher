using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;
using Azure.Cli.ContextSwitcher.Helpers;
using Azure.Cli.ContextSwitcher.Commands.Config;

namespace Azure.Cli.ContextSwitcher.Commands.Contexts
{
    internal class SetCurrentContextCommand : ConfigCommandBase
    {
        private AzureCliContext? _config;

        Option<string> _context = new Option<string>("--context", "The name of the context used to perform the login.");

        public SetCurrentContextCommand(string? description = null) : base("set", description)
        {
            _context.IsRequired = true;
            _context.AddAlias("-ctx");
            AddOption(_context);

            this.SetHandler((configFile, context) =>
            {
                _config = AzureCliContextManager.ReadConfig(configFile);
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
            _configFile, _context);
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
