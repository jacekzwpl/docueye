using MediatR;

namespace DocuEye.WorkspacesKeeper.Application.Events
{
    public class WorkspaceDeletedEvent : INotification
    {
        public string WorkspaceId { get; set; }

        public WorkspaceDeletedEvent(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
