using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    public interface IImportWorkspaceService
    {
        public Task<bool> Import(ImportWorkspaceParameters parameters);
    }
}
