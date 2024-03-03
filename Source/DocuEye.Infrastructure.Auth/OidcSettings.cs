namespace DocuEye.Infrastructure.Auth
{
    public class OidcSettings
    {
        public bool Enabled { get; set; } = false;
        public string Authority { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
        public IEnumerable<string> Scopes { get; set; } = Enumerable.Empty<string>();

        public string RolesClaim { get; set; } = "realm_access_roles";
    }
}
