namespace Azure.Cli.ContextSwitcher.Model
{
    public class Tenant
    {
        public string Name { get; set; } = string.Empty;
        public string DirectoryName { get; set; } = string.Empty;
        public string TenantId { get; set; } = string.Empty;
    }
}
