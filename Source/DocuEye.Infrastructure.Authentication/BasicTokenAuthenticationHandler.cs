using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace DocuEye.WorkspaceImporter.Api
{
    /// <summary>
    /// Basic authentication handler
    /// </summary>
    public class BasicTokenAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {

        private readonly IConfiguration config;

        public BasicTokenAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IConfiguration configuration)
            : base(options, logger, encoder)
        {
            this.config = configuration;
        }
        /// <summary>
        /// Handles authentication
        /// </summary>
        /// <returns>Authentication result</returns>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!this.Request.Headers.ContainsKey("Authorization"))
                return Task.FromResult(AuthenticateResult.Fail("Authorization failed"));

            var authHeaderValue = this.Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authHeaderValue))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization failed"));
            }

            var token = AuthenticationHeaderValue.Parse(authHeaderValue);

            if (token.Parameter != config["DocuEye:AdminToken"])
            {
                return Task.FromResult(AuthenticateResult.Fail("Invalid Authorization"));
            }


            var claims = new[] { new Claim("", "") };
            var identity = new ClaimsIdentity(claims, Scheme.Name);
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
