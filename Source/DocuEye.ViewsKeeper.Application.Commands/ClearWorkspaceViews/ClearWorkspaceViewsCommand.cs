using MediatR;

namespace DocuEye.ViewsKeeper.Application.Commands.ClearWorkspaceViews
{
    public class ClearWorkspaceViewsCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;

        public ClearWorkspaceViewsCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
