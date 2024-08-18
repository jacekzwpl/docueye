using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation
{
    public class ImportDocumentationResult : ImportResult
    {
        public ImportDocumentationResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ImportDocumentationResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
