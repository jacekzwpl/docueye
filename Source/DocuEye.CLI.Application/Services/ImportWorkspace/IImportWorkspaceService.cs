using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    /// <summary>
    /// Import workspace service interface
    /// </summary>
    public interface IImportWorkspaceService
    {
        /// <summary>
        /// Imports workspace
        /// </summary>
        /// <param name="parameters">Import workspace parameters</param>
        /// <returns>True if import finished with success. False otherwise.</returns>
        public Task<bool> Import(ImportWorkspaceParameters parameters);
    }
}
