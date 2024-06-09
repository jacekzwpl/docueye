using DocuEye.DocsKeeper.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : INotificationHandler<WorkspaceDeletedEvent>
    {
        private readonly IDocsKeeperDBContext dbContext;
        public WorkspaceDeletedEventHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.Decisions.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
            await this.dbContext.DocumentationFiles.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
            await this.dbContext.Images.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
            await this.dbContext.Documentations.DeleteManyAsync(x => x.WorkspaceId == notification.WorkspaceId);
        }
    }
}
