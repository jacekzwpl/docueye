using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetElement
{
    /// <summary>
    /// Handler for GetElementQuery
    /// </summary>
    public class GetElementQueryHandler : IQueryHandler<GetElementQuery, Element?>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetElementQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Element or null if no was found</returns>
        public async Task<Element?> HandleAsync(GetElementQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Elements
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
