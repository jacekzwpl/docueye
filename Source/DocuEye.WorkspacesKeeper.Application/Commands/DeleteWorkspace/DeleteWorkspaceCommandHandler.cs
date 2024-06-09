using DocuEye.WorkspacesKeeper.Application.Events;
using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand>
    {
        private readonly IMediator mediator;
        private readonly IWorkspacesKeeperDBContext dbContext;

        public DeleteWorkspaceCommandHandler(IMediator mediator, IWorkspacesKeeperDBContext dbContext)
        {
            this.mediator = mediator;
            this.dbContext = dbContext;
        }
        public async Task Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.ViewConfigurations.DeleteManyAsync(o => o.Id == request.WorkspaceId);
            await this.dbContext.Workspaces.DeleteManyAsync(o => o.Id == request.WorkspaceId);
            await this.mediator.Publish(new WorkspaceDeletedEvent(request.WorkspaceId), cancellationToken);
        }
    }
}
