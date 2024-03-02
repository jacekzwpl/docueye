namespace DocuEye.Web
{
    public class OidcSettings
    {
        public bool Enabled { get; set; } = false;
        public string Authority { get; set; } = string.Empty;
        public string ClientId { get; set; } = string.Empty;
        public string ClientSecret { get; set; } = string.Empty;
    }
}
