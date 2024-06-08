using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Commands.ClearWorkspaceData
{
    public class ClearWorkspaceDataCommandHandler : IRequestHandler<ClearWorkspaceDataCommand>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        public ClearWorkspaceDataCommandHandler(IWorkspacesKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task Handle(ClearWorkspaceDataCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.ViewConfigurations.DeleteManyAsync(o => o.Id == request.WorkspaceId);
            await this.dbContext.Workspaces.DeleteManyAsync(o => o.Id == request.WorkspaceId);
        }
    }
}
