using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DeleteOpenApiFile
{
    public interface IDeleteOpenApiFileService
    {
        Task<bool> DeleteOpenApiFile(string workspaceId, string? elementId = null, string? elementDslId = null);
    }
}
