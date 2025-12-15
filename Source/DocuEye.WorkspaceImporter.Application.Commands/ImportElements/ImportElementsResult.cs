using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportElements
{
    public class ImportElementsResult : ImportResult
    {
        public ImportElementsResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ImportElementsResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
