using Spectre.Console;
using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config.Contexts
{
    internal class DescribeContextsCommand : ConfigCommandBase
    {
        private const string DEFAULT_CONTEXT = "CurrentContext";

        private AzureCliContext _config;
        private Option<string> _context = new Option<string>("--context", "The name of the context used to perform the login. If not set, the current context is displayed.");

        public DescribeContextsCommand(string? description = null) : base("describe", description)
        {
            _context.IsRequired = false;
            _context.AddAlias("-ctx");
            _context.SetDefaultValue(DEFAULT_CONTEXT);
            AddOption(_context);

            this.SetHandler((configFile, context) =>
            {
                try
                {
                    var content = AzureCliContextManager.ReadConfig(configFile);
                    _config = AzureCliContextManager.ReadConfig(configFile);

                    if (context.Equals(DEFAULT_CONTEXT))
                    {
                        context = _config.CurrentContext;
                    }

                    var currentContext = _config.GetContext(context);
                    if (currentContext is null)
                    {
                        AnsiConsole.MarkupLine($"[lightgoldenrod2_1]Context [[{context}]] was not found in config.[/]");
                        return;
                    }

                    // Create a table
                    var table = new Table();
                    table.Border = TableBorder.Ascii2;
                    table.Title = new TableTitle($"[{Color.Aqua}]Users for config [[{configFile}]][/]");

                    // Add some columns
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Property[/]");
                    table.AddColumn($"[{Color.LightGoldenrod2_1}]Value[/]");

                    table.AddRow("Context", context);

                    if (_config.Tenants.TryGetValue(currentContext.Tenant, out Tenant tenant))
                    {
                        table.AddRow("Tenant Name", tenant.Name); //, item.Value.Name, item.Value.Tenant ?? $"[{Color.Grey}]n.a.[/]", item.Value.User ?? $"[{Color.Grey}]n.a.[/]");
                        table.AddRow("Tenant Id", tenant.TenantId);
                    }

                    table.AddEmptyRow();

                    if (_config.Users.TryGetValue(currentContext.User, out User user))
                    {
                        table.AddRow("User displayname", user.DisplayName);
                        table.AddRow("Logintype", user.LoginType.ToString());
                        table.AddRow("Username", user.Username ?? $"[{Color.Grey}]n.a.[/]");
                        table.AddRow("Password", string.IsNullOrEmpty(user.Password) ? $"[{Color.Grey}]n.a.[/]" : "**************");
                    }

                    // Render the table to the console
                    AnsiConsole.Write(table);
                }
                catch (ContextConfigurationException ex)
                {
                    AnsiConsole.MarkupLine($"[{Color.Maroon}]{ex.Message.EscapeMarkup()}[/]");
                }
            },
            _configFile, _context);
        }
    }
}
