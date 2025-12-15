using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportDecisionsLinks
{
    public class ImportDecisionsLinksResult : ImportResult
    {
        public ImportDecisionsLinksResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ImportDecisionsLinksResult(string workspaceId, bool isSuccess, string? message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
