using DocuEye.ChangeTracker.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : INotificationHandler<WorkspaceDeletedEvent>
    {
        private readonly IChangeTrackerDBContext dBContext;
        public WorkspaceDeletedEventHandler(IChangeTrackerDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dBContext.ModelChanges
                .DeleteManyAsync(o => o.WorkspaceId == notification.WorkspaceId);
        }
    }
}
