using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DeleteOpenApiFile
{
    public class DeleteOpenApiFileService : IDeleteOpenApiFileService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<DeleteOpenApiFileService> logger;

        public DeleteOpenApiFileService(IDocuEyeApiClient apiClient, ILogger<DeleteOpenApiFileService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }

        public async Task<bool> DeleteOpenApiFile(string workspaceId, string? elementId = null, string? elementDslId = null)
        {
            this.logger.LogInformation("Removing OpenAPI file...");
            var result = await this.apiClient.DeleteOpenApiFile(workspaceId, elementId, elementDslId);
            if (string.IsNullOrEmpty(result))
            {
                this.logger.LogInformation("Success deleting OpenAPI file.");
                return true;
            }
            this.logger.LogError("Failure deleting OpenAPI file. Message: {0}", result);
            return false;
        }
    }
}
