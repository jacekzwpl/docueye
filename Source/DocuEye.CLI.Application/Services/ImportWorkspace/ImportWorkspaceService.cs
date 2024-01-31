using DocuEye.CLI.ApiClient;
using System;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    public class ImportWorkspaceService : IImportWorkspaceService
    {

        private readonly IDocuEyeApiClient apiClient;

        public Task Import(string importKey, string workspaceFilePath, string? workspaceId, string? sourceLink)
        {
            throw new NotImplementedException();
        }
    }
}
