namespace Azure.Cli.ContextSwitcher.Model
{
    internal class AzureCliContext
    {
        public List<Tenant> Tenants { get; set; } = new List<Tenant>();

        public Dictionary<string, User> Users { get; set; } = new Dictionary<string, User>();

        public Dictionary<string, Context> Contexts { get; set; } = new Dictionary<string, Context>();

        public string CurrentContext { get; set; } = "";

        internal void AddTenant(string name, string tenantId, string? friendlyName = null)
        {
            var key = name.ToLower().Replace(" ", "-");
            if (Tenants.Any(t => t.Name.Equals(key)))
            {
                throw new ContextConfigurationException($"A tenant with the key [{key}] is already configured. Duplicate key names are not supported.");
            }

            Tenants.Add(new Tenant()
                        {
                            Name = key,
                            DirectoryName = friendlyName ?? name,
                            TenantId = tenantId
                        });
        }

        public void AddUser(string name, LoginType type = LoginType.Interactive, string? friendlyName = null, string? username = null, string? password = null)
        {
            var key = name.ToLower().Replace(" ", "-");
            if (Users.ContainsKey(key))
            {
                throw new ContextConfigurationException($"A user with the key [{key}] is already configured. Duplicate key names not supported.");
            }

            switch (type)
            {
                case LoginType.Interactive:
                    Users.Add(key, new User(friendlyName ?? name));
                    break;
                case LoginType.ServicePrincipal:
                case LoginType.UsernamePassword:
                    Users.Add(key, new User(friendlyName ?? name, type, username!, password!));
                    break;
                default:
                    break;
            }
        }

        public void AddContext(Context context)
        {
            if (context is null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (Contexts.ContainsKey(context.Tenant))
            {
                throw new ArgumentOutOfRangeException(nameof(context.Tenant), $"The tenant name [{context.Tenant}] was not found in the configuration.");
            }

            if (Contexts.ContainsKey(context.User))
            {
                throw new ArgumentOutOfRangeException(nameof(context.User), $"The user name [{context.User}]  was not found in the configuration.");
            }

            Contexts.Add(context.Name, context);
        }

        public Context GetContext(string name)
        {
            if (Contexts.ContainsKey(name))
            {
                return Contexts[name];
            }

            return null;
        }

        public static AzureCliContext CreateDefault()
        {
            return new AzureCliContext();
        }
    }
}
