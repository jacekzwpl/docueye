
using DocuEye.CLI.ApiClient.Model;
using System.Threading.Tasks;

namespace DocuEye.CLI.ApiClient
{
    /// <summary>
    /// DocueEye Ap client interface
    /// </summary>
    public interface IDocuEyeApiClient
    {
        /// <summary>
        /// Imports workspace
        /// </summary>
        /// <param name="request">Import workspace request</param>
        /// <returns>Import workspace result</returns>
        Task<ImportWorkspaceResult> ImportWorkspace(ImportWorkspaceRequest request);
    }
}
