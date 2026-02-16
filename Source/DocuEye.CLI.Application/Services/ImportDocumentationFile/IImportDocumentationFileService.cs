using DocuEye.CLI.Application.Services.ImportOpenApiFile;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.ImportDocumentationFile
{
    public interface IImportDocumentationFileService
    {
        public Task<bool> Import(ImportDocumentationFileParameters parameters);
    }
}
