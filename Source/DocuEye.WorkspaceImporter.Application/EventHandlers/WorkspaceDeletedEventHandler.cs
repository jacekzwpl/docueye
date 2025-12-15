using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.WorkspaceImporter.Persistence;
using DocuEye.WorkspacesKeeper.Application.Events;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.EventHandlers
{
    public class WorkspaceDeletedEventHandler : IEventHandler<WorkspaceDeletedEvent>
    {
        private readonly IWorkspaceImporterDBContext dbContext;
        public WorkspaceDeletedEventHandler(IWorkspaceImporterDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(WorkspaceDeletedEvent notification, CancellationToken cancellationToken)
        {
            await this.dbContext.WorkspaceImports.DeleteManyAsync(o => o.WorkspaceId == notification.WorkspaceId);
        }
    }
}
