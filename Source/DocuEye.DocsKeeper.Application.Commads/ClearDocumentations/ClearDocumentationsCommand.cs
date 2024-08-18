using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.ClearDocumentations
{
    public class ClearDocumentationsCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearDocumentationsCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
