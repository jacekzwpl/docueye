using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    public interface IImportWorkspaceService
    {
        public Task Import(string importKey, string workspaceFilePath, string? workspaceId, string? sourceLink);
    }
}
