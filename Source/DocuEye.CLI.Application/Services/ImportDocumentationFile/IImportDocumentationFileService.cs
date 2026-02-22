using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportDocumentationFile
{
    public interface IImportDocumentationFileService
    {
        public Task<bool> Import(ImportDocumentationFileParameters parameters);
    }
}
