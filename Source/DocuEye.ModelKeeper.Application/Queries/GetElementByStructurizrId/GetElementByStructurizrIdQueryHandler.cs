using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId
{
    public class GetElementByStructurizrIdQueryHandler : IQueryHandler<GetElementByStructurizrIdQuery, Element?>
    {
        private readonly IModelKeeperDBContext dbContext;

        public GetElementByStructurizrIdQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Element?> HandleAsync(GetElementByStructurizrIdQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Elements
                .FindOne(o => o.StructurizrId == request.StructurizrId && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
