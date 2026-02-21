using DocuEye.CLI.ApiClient;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DeleteDocumentationFile
{
    public class DeleteDocumentationFileService : IDeleteDocumentationFileService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<DeleteDocumentationFileService> logger;

        public DeleteDocumentationFileService(IDocuEyeApiClient apiClient, ILogger<DeleteDocumentationFileService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }

        public async Task<bool> DeleteDocumentaionFile(string workspaceId, string documentationType, string? elementId = null, string? elementDslId = null)
        {
            this.logger.LogInformation($"Removing {documentationType} file...");
            var result = await this.apiClient.DeleteDocumentationFile(workspaceId, documentationType, elementId, elementDslId);
            if (string.IsNullOrEmpty(result))
            {
                this.logger.LogInformation($"Success deleting {documentationType} file.");
                return true;
            }
            this.logger.LogError($"Failure deleting {documentationType} file. Message: {result}");
            return false;
        }
    }
}
