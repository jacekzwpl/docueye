using DocuEye.WorkspacesKeeper.Application.Events;
using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : INotificationHandler<WorkspaceDeletedEvent>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        public WorkspaceDeletedEventHandler(IWorkspacesKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.ViewConfigurations.DeleteManyAsync(o => o.Id == notification.WorkspaceId);
            await this.dbContext.Workspaces.DeleteManyAsync(o => o.Id == notification.WorkspaceId);
        }
    }
}
