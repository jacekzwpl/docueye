using DocuEye.ChangeTracker.Persistence;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.WorkspacesKeeper.Application.Events;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : IEventHandler<WorkspaceDeletedEvent>
    {
        private readonly IChangeTrackerDBContext dBContext;
        public WorkspaceDeletedEventHandler(IChangeTrackerDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task HandleAsync(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dBContext.ModelChanges
                .DeleteManyAsync(o => o.WorkspaceId == notification.WorkspaceId);
        }
    }
}
