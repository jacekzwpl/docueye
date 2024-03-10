using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient.Auth
{
    public class OidcHttpClient
    {
        private readonly HttpClient httpClient;
        private IDiscoveryCache? discoCache;
        public OidcHttpClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<string?> GetAccessToken(AuthenticationOptions authOptions)
        {
            if (string.IsNullOrEmpty(authOptions.OidcClientId))
            {
                throw new ArgumentException("OIDC client id is missing.");
            }
            if (string.IsNullOrEmpty(authOptions.OidcAuthority))
            {
                throw new ArgumentException("OIDC authority is missing.");
            }

            if (this.discoCache == null)
            {
                this.discoCache = new DiscoveryCache(authOptions.OidcAuthority);
            }

            

            var disco = await this.discoCache.GetAsync();
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await this.httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = authOptions.OidcClientId,
                ClientSecret = authOptions.OidcClientSecret,
                Scope = authOptions.OidcScopes
            });
            if (response.IsError) throw new Exception(response.Error);
            Console.WriteLine(response.AccessToken);
            return response.AccessToken;

        }
    }
}
