using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspacesKeeper.Application.Events;
using DocuEye.WorkspacesKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommandHandler : ICommandHandler<DeleteWorkspaceCommand>
    {
        private readonly IMediator mediator;
        private readonly IWorkspacesKeeperDBContext dbContext;

        public DeleteWorkspaceCommandHandler(IMediator mediator, IWorkspacesKeeperDBContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }
        public async Task HandleAsync(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.ViewConfigurations.DeleteManyAsync(o => o.Id == request.WorkspaceId);
            await this.dbContext.Workspaces.DeleteManyAsync(o => o.Id == request.WorkspaceId);
            await this.mediator.SendEventAsync(new WorkspaceDeletedEvent(request.WorkspaceId), cancellationToken);
        }
    }
}
