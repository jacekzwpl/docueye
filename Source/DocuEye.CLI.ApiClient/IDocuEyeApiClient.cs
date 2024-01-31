
using DocuEye.CLI.ApiClient.Model;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient
{
    public interface IDocuEyeApiClient
    {
        Task<ImportWorkspaceResult> ImportWorkspace(ImportWorkspaceRequest request);
    }
}
