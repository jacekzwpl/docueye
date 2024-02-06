using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportOpenApiFile
{
    public interface IImportOpenApiFileService
    {
        public Task<bool> Import(ImportOpenApiFileParameters parameters);
    }
}
