using Azure.Cli.ContextSwitcher.Model.Serialization;
using System.Text.Json.Serialization;

namespace Azure.Cli.ContextSwitcher.Model
{
    public class User
    {
        public User()
        {

        }

        /// <summary>
        /// Creates a new instance of a User with an Interactive LoginType
        /// </summary>
        /// <param name="name"></param>
        public User(string name, string? description = null)
        {
            Name = name;
            LoginType = LoginType.Interactive;
            Description = description;
        }

        /// <summary>
        /// Creates a new instance of a User with a specific LoginType
        /// </summary>
        /// <param name="name"></param>
        public User(string name, LoginType loginType, string username, string passwword, string? description = null)
        {
            Name = name;
            Description = description;
            LoginType = loginType;

            if (loginType != LoginType.Interactive)
            {
                if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(passwword))
                {
                    throw new ArgumentNullException($"{nameof(User)}, {nameof(passwword)}", "When using a non-interactive login, username and password are required.");
                }

                Username = username;
                Password = passwword;
            }
        }

        public string Name { get; init; } = string.Empty;

        public string? Description { get; init; }

        [JsonConverter(typeof(LoginTypeConverter))]
        public LoginType LoginType { get; set; }
        public string? Username { get; set; } = null;
        public string? Password { get; set; } = null;

    }
}
