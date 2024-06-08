using MediatR;

namespace DocuEye.ChangeTracker.Application.Commands.ClearWorkspaceEvents
{
    public class ClearWorkspaceEventsCommand : IRequest
    {
        public string WorkspaceId { get; set; } = null!;
        public ClearWorkspaceEventsCommand(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
