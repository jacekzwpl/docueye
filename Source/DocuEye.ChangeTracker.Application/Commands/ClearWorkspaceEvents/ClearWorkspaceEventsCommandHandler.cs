using DocuEye.ChangeTracker.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.Commands.ClearWorkspaceEvents
{
    public class ClearWorkspaceEventsCommandHandler : IRequestHandler<ClearWorkspaceEventsCommand>
    {
        private readonly IChangeTrackerDBContext dBContext;
        public ClearWorkspaceEventsCommandHandler(IChangeTrackerDBContext dBContext)
        {
            this.dBContext = dBContext;
        }
        public async Task Handle(ClearWorkspaceEventsCommand request, CancellationToken cancellationToken)
        {
            await this.dBContext.ModelChanges
                .DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
        }
    }
}
