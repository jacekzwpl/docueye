using DocuEye.WorkspacesKeeper.Application.Events;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace
{
    public class DeleteWorkspaceCommandHandler : IRequestHandler<DeleteWorkspaceCommand>
    {
        private readonly IMediator mediator;

        public DeleteWorkspaceCommandHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public async Task Handle(DeleteWorkspaceCommand request, CancellationToken cancellationToken)
        {
            await this.mediator.Publish(new WorkspaceDeletedEvent(request.WorkspaceId), cancellationToken);
        }
    }
}
