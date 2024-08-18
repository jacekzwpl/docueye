using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.ClearImages
{
    public class ClearImagesCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearImagesCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
