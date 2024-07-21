using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DeleteWorkspace
{
    public interface IDeleteWorkspaceService
    {
        Task<bool> DeleteWorkspace(string workspaceId);
    }
}
