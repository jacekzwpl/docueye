using DocuEye.CLI.ApiClient.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System;
using System.Collections.Generic;
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

        public async Task<string?> CompatibilityInfo()
        {
            var message = new HttpRequestMessage(HttpMethod.Get, "api/app/compatibility/info");
            using (var response = await this.httpClient.SendAsync(message))
            {
                if (response.IsSuccessStatusCode && response.Content.Headers.ContentType?.MediaType == "application/json")
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<CompatibilityInfoResponse>(responseBody, this.serializerOptions);
                    if (data == null)
                    {
                        return null;
                    }
                    return data.AcceptedVersions;
                }
                else
                {
                    return null;
                }
            }
        }

        public async Task<string?> DeleteOpenApiFile(string workspaceId, string? elementId = null, string? elementDslId = null)
        {
            string baseUrl = string.Format("api/workspaces/{0}/docfile/openapi", workspaceId);
            var parts = new List<string>();

            if (!string.IsNullOrEmpty(elementId))
                parts.Add($"elementId={Uri.EscapeDataString(elementId)}");

            if (!string.IsNullOrEmpty(elementDslId))
                parts.Add($"elementDslId={Uri.EscapeDataString(elementDslId)}");
            string url = parts.Count == 0 ? baseUrl : $"{baseUrl}?{string.Join("&", parts)}";
            var message = new HttpRequestMessage(HttpMethod.Delete, url);
            using (var response = await this.httpClient.SendAsync(message))
            {
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var problemData = JsonSerializer.Deserialize<ProblemDetailsResponse>(responseBody, this.serializerOptions);
                    if (problemData == null)
                    {
                        return "Delete openapi file failed. Reason of failure is unkonown";
                    }
                    return problemData.Detail ?? problemData.Title;
                }
                else
                {
                    return "Delete openapi file failed. Reason of failure is unkonown";
                }
            }
        }

        public async Task<string?> DeleteWorkspace(string workspaceId)
        {
            var message = new HttpRequestMessage(HttpMethod.Delete, string.Format("api/workspaces/{0}", workspaceId));
            using (var response = await this.httpClient.SendAsync(message))
            {
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }
                else if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var problemData = JsonSerializer.Deserialize<ProblemDetailsResponse>(responseBody, this.serializerOptions);
                    if (problemData == null)
                    {
                        return "Delete workspace failed. Reason of failure is unkonown";
                    }
                    return problemData.Detail ?? problemData.Title;
                }
                else
                {
                    return "Delete workspace failed. Reason of failure is unkonown";
                }
            }
        }

        public async Task<ImportWorkspaceResponse> ImportClearDecisions(ImportClearDecisionsRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/cleardecisions");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportClearDocItems(ImportClearDocItemsRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, "/api/workspaces/import/cleardocitems");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportDecision(ImportDecisionRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/decision");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportDecisionLinks(ImportDecisionsLinksRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/decisionlinks");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportDocumentation(ImportDocumentationRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/documentation");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportElements(ImportElementsRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/elements");
            var str = JsonSerializer.Serialize(data, this.serializerOptions);
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportFinish(ImportFinalizeRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/finish");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportImage(ImportImageRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/image");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportIssues(ImportIssuesRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/issues");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<string?> ImportOpenApiFile(string workspaceId, ImportOpenApiFileRequest request)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, string.Format("api/workspaces/{0}/docfile/openapi", workspaceId));
            message.Content = new StringContent(JsonSerializer.Serialize(request, this.serializerOptions), Encoding.UTF8, "application/json");
            using (var response = await this.httpClient.SendAsync(message))
            {
                if (response.IsSuccessStatusCode)
                {
                    return null;
                }else if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var problemData = JsonSerializer.Deserialize<ProblemDetailsResponse>(responseBody, this.serializerOptions);
                    if (problemData == null)
                    {
                        return "Import failed. Reason of failure is unkonown";
                    }
                    return problemData.Detail ?? problemData.Title;
                }else
                {
                    return "Import failed. Reason of failure is unkonown";
                }
            }
        }

        public async Task<ImportWorkspaceResponse> ImportRelationships(ImportRelationshipsRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/relationships");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportStart(ImportWorkspaceStartRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/start");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportViewConfiguration(ImportViewConfigurationRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/viewconfiguration");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        public async Task<ImportWorkspaceResponse> ImportViews(ImportViewsRequest data)
        {
            var message = new HttpRequestMessage(HttpMethod.Put, "/api/workspaces/import/views");
            message.Content = new StringContent(JsonSerializer.Serialize(data, this.serializerOptions), Encoding.UTF8, "application/json");
            return await this.ImportAction(message);
        }

        private async Task<ImportWorkspaceResponse> ImportAction(HttpRequestMessage message)
        {
            using (var response = await this.httpClient.SendAsync(message))
            {

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var data = JsonSerializer.Deserialize<ImportWorkspaceResponse>(responseBody, this.serializerOptions);
                    if (data == null)
                    {
                        return new ImportWorkspaceResponse()
                        {
                            IsSuccess = false,
                            Message = "There was no conntent in response. Import staus is unknown."
                        };
                    }
                    return data;
                }
                else if (response.Content.Headers.ContentType?.MediaType == "application/problem+json")
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var problemData = JsonSerializer.Deserialize<ProblemDetailsResponse>(responseBody, this.serializerOptions);
                    if (problemData == null)
                    {
                        return new ImportWorkspaceResponse()
                        {
                            IsSuccess = false,
                            Message = "There was no conntent in response. Import staus is unknown"
                        };
                    }
                    var responseMessage = problemData.Detail ?? problemData.Title;
                    if(problemData.Errors != null)
                    {
                        foreach(var error in problemData.Errors)
                        {
                            responseMessage += Environment.NewLine + $"{error.Key}: {string.Join(", ", error.Value)}";
                        }
                    }

                    return new ImportWorkspaceResponse()
                    {
                        IsSuccess = false,
                        Message = responseMessage
                    };
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    var responseData = JsonSerializer.Deserialize<ImportWorkspaceResponse>(responseBody, this.serializerOptions);
                    if (responseData == null)
                    {
                        return new ImportWorkspaceResponse()
                        {
                            IsSuccess = false,
                            Message = "There was no conntent in response. Import staus is unknown"
                        };
                    }
                    return responseData;
                }
                else
                {
                    
                    return new ImportWorkspaceResponse()
                    {
                        IsSuccess = false,
                        Message = "There was no conntent in response. Import staus is unknown"
                    };
                }
            }
        }

    }
}
