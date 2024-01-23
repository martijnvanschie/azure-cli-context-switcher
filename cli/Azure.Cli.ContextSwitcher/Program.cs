// See https://aka.ms/new-console-template for more information
using Azure.Cli.ContextSwitcher.Commands;
using Spectre.Console;
using System.Diagnostics;

try
{
    var manager = new CommandsManager();
    manager.Invoke(args);
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}
