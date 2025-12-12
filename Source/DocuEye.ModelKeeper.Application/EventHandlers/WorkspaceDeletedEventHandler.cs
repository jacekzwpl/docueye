using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.ModelKeeper.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : IEventHandler<WorkspaceDeletedEvent>
    {
        private readonly IModelKeeperDBContext dbContext;
        public WorkspaceDeletedEventHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.Elements.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
            await this.dbContext.Relationships.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
        }
    }
}
