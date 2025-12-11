using DocuEye.DocsKeeper.Persistence;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocuEye.DocsKeeper.Model.Maps;
using DocuEye.DocsKeeper.Application.Model;
using DocuEye.Infrastructure.Mediator.Queries;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionsList
{
    public class GetDecisionsListQueryHandler : IQueryHandler<GetDecisionsListQuery, IEnumerable<DecisionsListItem>>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">IMapper service</param>
        public GetDecisionsListQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of decisions</returns>
        public async Task<IEnumerable<DecisionsListItem>> HandleAsync(GetDecisionsListQuery request, CancellationToken cancellationToken)
        {
            var decisions = await this.dbContext.Decisions
                .Find(
                    o => o.WorkspaceId == request.WorkspaceId);
            return decisions.MapToDecisionsListItems();
        }
    }
}
