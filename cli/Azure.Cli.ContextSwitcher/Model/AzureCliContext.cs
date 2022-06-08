﻿namespace Azure.Cli.ContextSwitcher.Model
{
    internal class AzureCliContext
    {
        public Dictionary<string, Tenant> Tenants { get; set; } = new Dictionary<string, Tenant>();

        public Dictionary<string, User> Users { get; set; } = new Dictionary<string, User>();

        public Dictionary<string, Context> Contexts { get; set; } = new Dictionary<string, Context>();

        public string CurrentContext { get; set; } = "";

        internal void AddTenant(string name, string tenantId, string? friendlyName = null)
        {
            if (Tenants.ContainsKey(name))
            {
                throw new ContextConfigurationException($"A tenant with the name [{name}] is already configured. Duplicate names not supported.");
            }

            Tenants.Add(name,
                new Tenant()
                    {
                        Name = friendlyName ?? name,
                        TenantId = tenantId
                    }
                );
        }

        public void AddUser(string name, LoginType type = LoginType.Interactive, string? friendlyName = null, string? username = null, string? password = null)
        {
            if (Users.ContainsKey(name))
            {
                throw new ContextConfigurationException($"A user with the name [{name}] is already configured. Duplicate names not supported.");
            }

            switch (type)
            {
                case LoginType.Interactive:
                    Users.Add(name, new User(friendlyName ?? name));
                    break;
                case LoginType.ServicePrincipal:
                    break;
                case LoginType.UsernamePassword:
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
