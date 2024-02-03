using Azure.Cli.ContextSwitcher.Helpers;

namespace Azure.Cli.ContextSwitcher.Model
{
    internal class AzureCliContext
    {
        public List<Tenant> Tenants { get; set; } = new List<Tenant>();

        public List<User> Users { get; set; } = new List<User>();

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
            if (Users.Any(t => t.Name.Equals(key)))
            {
                throw new ContextConfigurationException($"A user with the key [{key}] is already configured. Duplicate key names not supported.");
            }

            switch (type)
            {
                case LoginType.Interactive:
                    Users.Add(new User(key));
                    break;
                case LoginType.ServicePrincipal:
                case LoginType.UsernamePassword:
                    Users.Add(new User(key, type, username!, password!));
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

        internal static void PerformLogin(Context currentContext, AzureCliContext _config)
        {
            var tenant = _config.Tenants.FirstOrDefault(t => t.Name.Equals(currentContext.Tenant));
            if (tenant == null)
            {
                throw new ContextConfigurationException($"Invalid configuration for context [{currentContext}]. No tenant found in configuration that matched the name [{currentContext.Tenant}].");
            }

            var user = _config.Users.FirstOrDefault(u => u.Name.Equals(currentContext.User));
            if (user == null)
            {
                throw new ContextConfigurationException($"Invalid configuration for context [{currentContext}]. No user found in configuration that matched the name [{currentContext.User}].");
            }

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
