using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Commands.ClearWorkspaceModel
{
    public class ClearWorkspaceModelCommandHandler : IRequestHandler<ClearWorkspaceModelCommand>
    {
        private readonly IModelKeeperDBContext dbContext;
        public ClearWorkspaceModelCommandHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task Handle(ClearWorkspaceModelCommand request, CancellationToken cancellationToken)
        {
            await this.dbContext.Elements.DeleteManyAsync(x => x.WorkspaceId == request.WorkspaceId);
            await this.dbContext.Relationships.DeleteManyAsync(x => x.WorkspaceId == request.WorkspaceId);
        }
    }
}
