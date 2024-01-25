using System.Text.Json;

namespace Azure.Cli.ContextSwitcher.Model
{
    internal class AzureCliContextManager
    {
        private const string APP_FOLDER = ".azctx";
        public const string DEFAULT_CONFIG_NAME = "config";

        internal static AzureCliContext ReadConfig(string? path = null)
        {
            var configName = path ?? DEFAULT_CONFIG_NAME;

            FileInfo file = GetConfigFile(configName);
            if (file.Exists == false)
            {
                throw new FileNotFoundException($"Unable to load configuration. The file [{path}] could not be found.");
            }

            string jsonString = File.ReadAllText(file.FullName);
            return JsonSerializer.Deserialize<AzureCliContext>(jsonString);
        }

        internal static string ReadConfigAsString(string configName = DEFAULT_CONFIG_NAME)
        {
            FileInfo file = GetConfigFile(configName);
            if (file.Exists == false)
            {
                return "{}";
            }

            return File.ReadAllText(file.FullName);
        }

        internal static void WriteConfig(AzureCliContext context, string configName = DEFAULT_CONFIG_NAME)
        {
            FileInfo file = GetConfigFile(configName);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull,
                WriteIndented = true
            };

            File.WriteAllText(file.FullName, JsonSerializer.Serialize(context, options));
        }

        internal static DirectoryInfo GetConfigFolder()
        {
            var configFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                APP_FOLDER
                );

            var dir = new DirectoryInfo(configFolder);

            if (dir.Exists == false)
            {
                dir.Create();
            }

            return dir;
        }

        internal static FileInfo GetConfigFile(string? contextName)
        {
            var configName = contextName ?? DEFAULT_CONFIG_NAME;
            var dir = GetConfigFolder();
            var path = Path.Combine(dir.FullName, configName);
            return new FileInfo(path);
        }
    }
}
