using Azure.Cli.ContextSwitcher.Model.Serialization;
using System.Text.Json.Serialization;

namespace Azure.Cli.ContextSwitcher.Model
{
    public class User
    {
        public User()
        {

        }

        public User(string name)
        {
            DisplayName = name;
            LoginType = LoginType.Interactive;
        }

        public string DisplayName { get; init; }

        [JsonConverter(typeof(LoginTypeConverter))]
        public LoginType LoginType { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }

    }
}
