using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementByStructurizrId
{
    public class GetElementByStructurizrIdQueryHandler : IRequestHandler<GetElementByStructurizrIdQuery, Element?>
    {
        private readonly IModelKeeperDBContext dbContext;

        public GetElementByStructurizrIdQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Element?> Handle(GetElementByStructurizrIdQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Elements
                .FindOne(o => o.DslId == request.StructurizrId && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
