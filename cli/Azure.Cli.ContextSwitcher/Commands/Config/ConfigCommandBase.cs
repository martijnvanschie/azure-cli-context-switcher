using System.CommandLine;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Commands.Config
{
    internal class ConfigCommandBase : CommandBase
    {
        internal Option<string> _configFile = new Option<string>("--cliconfig");

        public ConfigCommandBase(string name, string? description = null) : base(name, description)
        {
            _configFile = new Option<string>("--cliconfig", () => AzureCliContextManager.DEFAULT_CONFIG_NAME, description: "The name of the cli config to use.");
            _configFile.AddAlias("-c");
            _configFile.ArgumentHelpName = " ";
            _configFile.IsRequired = false;
            AddOption(_configFile);
        }
    }
}
