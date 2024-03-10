namespace DocuEye.CLI.ApiClient.Auth
{
    public class AuthenticationOptions
    {
        public string? AdminToken { get; private set; }
        public string? OidcAuthority { get; private set; }
        public string? OidcClientId { get; private set; }
        public string? OidcClientSecret { get; private set; }
        public string OidcScopes { get; private set; }

        public AuthenticationOptions(string? adminToken, string? oidcAuthority, string? oidcClientId, string? oidcClientSecret, string? oidcScopes)
        {
            this.AdminToken = adminToken;
            this.OidcAuthority = oidcAuthority;
            this.OidcClientId = oidcClientId;
            this.OidcClientSecret = oidcClientSecret;
            this.OidcScopes = string.IsNullOrEmpty(oidcScopes) ? "docueye.import" : oidcScopes;
        }
    }
}
