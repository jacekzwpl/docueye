using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Net.Http.Headers;

namespace DocuEye.CLI.ApiClient.Auth
{
    public class AuthenticationHandler : DelegatingHandler
    {

        private readonly AuthenticationOptions authOptions;
        private readonly OidcHttpClient oidcClient;

        public AuthenticationHandler(AuthenticationOptions authOptions, OidcHttpClient oidcClient)
        {
            this.authOptions = authOptions;
            this.oidcClient = oidcClient;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(this.authOptions.AdminToken))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", this.authOptions.AdminToken);
            }
            else
            {
                var token = await this.oidcClient.GetAccessToken(this.authOptions);
                if (token == null)
                {
                    throw new Exception("Access token is empty.");
                }
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
