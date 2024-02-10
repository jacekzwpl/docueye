using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementByDslId
{
    /// <summary>
    /// Gets element by structurizr dsl id
    /// </summary>
    public class GetElementByDslIdQueryHandler : IRequestHandler<GetElementByDslIdQuery, Element?>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetElementByDslIdQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Element or null if no was found</returns>
        public async Task<Element?> Handle(GetElementByDslIdQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Elements
                .FindOne(o => o.DslId == request.DslId && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
