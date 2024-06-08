using DocuEye.ModelKeeper.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : INotificationHandler<WorkspaceDeletedEvent>
    {
        private readonly IModelKeeperDBContext dbContext;
        public WorkspaceDeletedEventHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.Elements.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
            await this.dbContext.Relationships.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
        }
    }
}
