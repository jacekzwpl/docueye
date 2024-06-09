using DocuEye.ViewsKeeper.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.EventHanlders
{
    public class WorkspaceDeletedEventHandler : INotificationHandler<WorkspaceDeletedEvent>
    {
        private readonly IViewsKeeperDBContext dbContext;
        public WorkspaceDeletedEventHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.AllViews.DeleteManyAsync(o => o.WorkspaceId == notification.WorkspaceId);
        }
    }
}
