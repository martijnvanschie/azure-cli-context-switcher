using System.CommandLine;

namespace Azure.Cli.ContextSwitcher.Commands
{
    internal class CommandBase : Command
    {
        public CommandBase(string name, string? description = null) : base(name, description)
        {
            
        }
    }
}
