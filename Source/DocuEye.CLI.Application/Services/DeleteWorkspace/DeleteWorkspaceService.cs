using DocuEye.CLI.ApiClient;
using DocuEye.CLI.Application.Services.ImportWorkspace;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DeleteWorkspace
{
    public class DeleteWorkspaceService : IDeleteWorkspaceService
    {
        private readonly IDocuEyeApiClient apiClient;
        private readonly ILogger<ImportWorkspaceService> logger;

        public DeleteWorkspaceService(IDocuEyeApiClient apiClient, ILogger<ImportWorkspaceService> logger)
        {
            this.apiClient = apiClient;
            this.logger = logger;
        }


        public async Task<bool> DeleteWorkspace(string workspaceId)
        {
            
            this.logger.LogInformation("Removing workspace");
            var result = await this.apiClient.DeleteWorkspace(workspaceId);


            if (string.IsNullOrEmpty(result))
            {
                this.logger.LogInformation("Import finished with success");
                return true;
            }
            else
            {
                this.logger.LogInformation("Import finished with failure.");
                this.logger.LogInformation("Import result message = {0}", result);
                return false;
            }

        }
    }
}
