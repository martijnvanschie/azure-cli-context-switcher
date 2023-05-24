using System.Text;
using System.Diagnostics;
using Azure.Cli.ContextSwitcher.Model;

namespace Azure.Cli.ContextSwitcher.Helpers
{
    /// <summary>
    /// https://duanenewman.net/blog/post/running-powershell-scripts-from-csharp/
    /// </summary>
    internal class Utilities
    {
        public static void LoginInteractive(Tenant tenant, string output = "none")
        {
            StringBuilder command = new StringBuilder();
            command.AppendLine(@$"az login --tenant {tenant.TenantId} -o {output}");
            command.AppendLine("az account show --query user");

            ExecutePowershellCommand(command.ToString());
        }

        public static void LoginServicePrincipal(Tenant tenant, User user, string output = "none")
        {
            StringBuilder command = new StringBuilder();
            command.AppendLine(@$"az login --service-principal -u {user.Username} -p {user.Password} --tenant {tenant.TenantId} -o {output}");
            command.AppendLine("az account show --query user");

            ExecutePowershellCommand(command.ToString());
        }

        public static void LoginUsernamePassword(Tenant tenant, User user, string output = "none")
        {
            var psCommmand = @$"az login -u {user.Username} -p {user.Password} --tenant {tenant.TenantId} -o {output}";
            ExecutePowershellCommand(psCommmand);
        }

        private static void ExecutePowershellCommand(string psCommmand)
        {
            var psCommandBytes = Encoding.Unicode.GetBytes(psCommmand);
            var psCommandBase64 = Convert.ToBase64String(psCommandBytes);

            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -ExecutionPolicy unrestricted -EncodedCommand {psCommandBase64}",
                UseShellExecute = false
            };
            var p = Process.Start(startInfo);
            p.WaitForExit();

            if (p.ExitCode != 0)
            {
                throw new Exception($"Exit code [{p.ExitCode}] while performing command [{psCommmand}].");
            }
        }
    }
}
