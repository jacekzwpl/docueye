
using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.DocsKeeper.Application.Commads.ClearDocumentations
{
    public class ClearDocumentationsCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearDocumentationsCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
