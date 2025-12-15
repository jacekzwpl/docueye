using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.ClearDecisions
{
    public class ClearDecisionsResult : ImportResult
    {
        public ClearDecisionsResult(string workspaceId, bool isSuccess) : base(workspaceId, isSuccess)
        {
        }

        public ClearDecisionsResult(string workspaceId, bool isSuccess, string message) : base(workspaceId, isSuccess, message)
        {
        }
    }
}
