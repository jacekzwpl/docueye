using DocuEye.Infrastructure.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.DeleteOpenApiFile
{
    public class DeleteOpenApiFileCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;
        public string ElementId { get; set; } = null!;

        public DeleteOpenApiFileCommand(string workspaceId, string elementId)
        {
            this.WorkspaceId = workspaceId;
            this.ElementId = elementId;
        }
    }
}
