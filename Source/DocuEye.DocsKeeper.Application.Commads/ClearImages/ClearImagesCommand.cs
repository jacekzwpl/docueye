
using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.DocsKeeper.Application.Commads.ClearImages
{
    public class ClearImagesCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearImagesCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
