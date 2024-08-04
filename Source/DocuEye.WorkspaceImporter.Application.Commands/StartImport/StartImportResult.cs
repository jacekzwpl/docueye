using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.StartImport
{
    public class StartImportResult : ImportResult
    {
        public StartImportResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public StartImportResult(string workspaceId, bool isSuccess, string message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
