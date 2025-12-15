

using DocuEye.Infrastructure.Mediator.Commands;


namespace DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;
        public DeleteWorkspaceCommand(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
