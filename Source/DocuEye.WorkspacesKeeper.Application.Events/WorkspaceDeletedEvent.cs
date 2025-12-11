

using DocuEye.Infrastructure.Mediator.Events;

namespace DocuEye.WorkspacesKeeper.Application.Events
{
    public class WorkspaceDeletedEvent : IEvent
    {
        public string WorkspaceId { get; set; }

        public WorkspaceDeletedEvent(string workspaceId)
        {
            WorkspaceId = workspaceId;
        }
    }
}
