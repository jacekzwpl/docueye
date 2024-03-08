using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient.Auth
{
    public class OidcHttpClient
    {
        private readonly HttpClient httpClient;
        public OidcHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string> GetAccessToken()
        {
            return string.Empty;
        }
    }
}
