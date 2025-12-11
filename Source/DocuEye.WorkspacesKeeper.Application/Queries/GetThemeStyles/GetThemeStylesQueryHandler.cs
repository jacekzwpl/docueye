
using DocuEye.Infrastructure.Mediator.Queries;
using System;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetThemeStyles
{
    /// <summary>
    /// Handler for GetThemeStylesQuery
    /// </summary>
    public class GetThemeStylesQueryHandler : IQueryHandler<GetThemeStylesQuery, ThemeStylesResult?>
    {
        private readonly IHttpClientFactory httpClientFactory;
        /// <summary>
        /// Creates instance 
        /// </summary>
        /// <param name="httpClientFactory">Http client factory</param>
        public GetThemeStylesQueryHandler(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Theme styles or null if there was an error getting styles</returns>
        public async Task<ThemeStylesResult?> HandleAsync(GetThemeStylesQuery request, CancellationToken cancellationToken)
        {
            var serializerOptions = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true
            };

            var client = this.httpClientFactory.CreateClient("ThemesClient");
            var response = await client.GetAsync(request.Url, cancellationToken);

            var uri = new Uri(request.Url);
            var baseUrl = string.Format("{0}://{1}{2}", 
                uri.Scheme,
                uri.Host,
                string.Join("", uri.Segments.Take(uri.Segments.Length - 1).ToArray())
                );
            
            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                var data = JsonSerializer.Deserialize<ThemeStylesResult>(responseBody, serializerOptions);
                if (data?.Elements != null)
                {
                    foreach (var item in data.Elements) { 
                        if(item.Icon != null)
                        {
                            item.Icon = string.Format("{0}{1}", baseUrl, item.Icon);
                        }
                    }
                }
                return data;
            }
            return null;
        }
    }
}
