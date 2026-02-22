using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.CLI.Application.Services.DeleteDocumentationFile
{
    public interface IDeleteDocumentationFileService
    {
        Task<bool> DeleteDocumentaionFile(string workspaceId, string documentationType, string? elementId = null, string? elementDslId = null);
    }
}
