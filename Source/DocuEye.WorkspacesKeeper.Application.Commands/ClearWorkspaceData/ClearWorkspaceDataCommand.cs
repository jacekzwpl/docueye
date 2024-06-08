using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Commands.ClearWorkspaceData
{
    public class ClearWorkspaceDataCommand : IRequest
    {
        public string WorkspaceId { get; set; }
        public ClearWorkspaceDataCommand(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
