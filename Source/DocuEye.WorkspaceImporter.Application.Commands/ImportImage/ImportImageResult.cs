using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportImage
{
    public class ImportImageResult : ImportResult
    {
        public ImportImageResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ImportImageResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
