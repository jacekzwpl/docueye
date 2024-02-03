using DocuEye.CLI.ApiClient.Model;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient
{
    /// <summary>
    /// DocuEye Api client
    /// </summary>
    public class DocuEyeApiClient : IDocuEyeApiClient
    {
        private readonly HttpClient httpClient;
        private JsonSerializerOptions serializerOptions;
        /// <summary>
        /// Creates new instance
        /// </summary>
        /// <param name="httpClient">HttpClient instance</param>
        public DocuEyeApiClient(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.serializerOptions = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString,
                PropertyNameCaseInsensitive = true
            };
        }

        /// <inheritdoc />
        public async Task<ImportWorkspaceResult> ImportWorkspace(ImportWorkspaceRequest request)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import");
            message.Content = new StringContent(JsonSerializer.Serialize(request, this.serializerOptions), Encoding.UTF8, "application/json");
            using (var response = await this.httpClient.SendAsync(message))
            {

                if(response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<ImportWorkspaceResult>(responseBody, this.serializerOptions);
                    if (data == null)
                    {
                        return new ImportWorkspaceResult()
                        {
                            IsSuccess = false,
                            Message = "There was no conntent in response. Import staus is unknown."
                        };
                    }
                    return data;
                }else if(response.Content.Headers.ContentType?.MediaType == "application/problem+json")
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var problemData = JsonSerializer.Deserialize<ProblemDetailsResponse>(responseBody, this.serializerOptions);
                    if(problemData == null)
                    {
                        return new ImportWorkspaceResult()
                        {
                            IsSuccess = false,
                            Message = "There was no conntent in response. Import staus is unknown"
                        };
                    }
                    return new ImportWorkspaceResult()
                    {
                        IsSuccess = false,
                        Message = problemData.Detail ?? problemData.Title
                    };
                }else
                {
                    return new ImportWorkspaceResult()
                    {
                        IsSuccess = false,
                        Message = "There was no conntent in response. Import staus is unknown"
                    };
                }
            }
        }
    }
}
