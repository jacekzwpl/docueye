using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.ClearWorkspaceDocs
{
    public class ClearWorkspaceDocsCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearWorkspaceDocsCommand(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
