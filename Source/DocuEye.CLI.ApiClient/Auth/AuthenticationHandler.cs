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

        public AuthenticationHandler(AuthenticationOptions authOptions)
        {
            this.authOptions = authOptions;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {


            if (string.IsNullOrEmpty(this.authOptions.AdminToken))
            {
                //var authMessage = base.SendAsync
            }
            else
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Basic", this.authOptions.AdminToken);
            }


            return await base.SendAsync(request, cancellationToken);
        }
    }
}
