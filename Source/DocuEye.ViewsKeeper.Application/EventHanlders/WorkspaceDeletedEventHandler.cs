using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.ViewsKeeper.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.EventHanlders
{
    public class WorkspaceDeletedEventHandler : IEventHandler<WorkspaceDeletedEvent>
    {
        private readonly IViewsKeeperDBContext dbContext;
        public WorkspaceDeletedEventHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.AllViews.DeleteManyAsync(o => o.WorkspaceId == notification.WorkspaceId);
        }
    }
}
