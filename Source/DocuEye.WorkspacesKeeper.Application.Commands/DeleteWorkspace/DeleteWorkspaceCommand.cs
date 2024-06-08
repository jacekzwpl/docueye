using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;
        public DeleteWorkspaceCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
