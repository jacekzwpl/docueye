using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Infrastructure.Authentication.OIDC
{
    public class OidcSettings
    {
        public string? Authority { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string Scopes { get; set; }
        public string ClaimType { get; set; }

        public OidcSettings() { 
        
            this.Scopes = "openid,profile,email";
            this.ClaimType = "email";
        }


        public string[] GetScopes()
        {
            return Scopes.Split(',', StringSplitOptions.RemoveEmptyEntries);
        }
    }
}
