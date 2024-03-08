namespace DocuEye.CLI.ApiClient.Auth
{
    public class AuthenticationOptions
    {
        public string? AdminToken { get; private set; }
        public string? OidcAuthority { get; private set; }
        public string? OidcClientId { get; private set; }
        public string? OiidcClientSecret { get; private set; }

        public AuthenticationOptions(string? adminToken, string? oidcAuthority, string? oidcClientId, string? oidcClientSecret)
        {
            this.AdminToken = adminToken;
            this.OidcAuthority = oidcAuthority;
            this.OidcClientId = oidcClientId;
            this.OiidcClientSecret = oidcClientSecret;
        }
    }
}
