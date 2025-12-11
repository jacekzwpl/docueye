using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocuEye.DocsKeeper.Model.Maps;
using DocuEye.DocsKeeper.Application.Model;

namespace DocuEye.DocsKeeper.Application.Queries.GetDecisionsList
{
    public class GetDecisionsListQueryHandler : IRequestHandler<GetDecisionsListQuery, IEnumerable<DecisionsListItem>>
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
        public async Task<IEnumerable<DecisionsListItem>> Handle(GetDecisionsListQuery request, CancellationToken cancellationToken)
        {
            var decisions = await this.dbContext.Decisions
                .Find(
                    o => o.WorkspaceId == request.WorkspaceId);
            return decisions.MapToDecisionsListItems();
        }
    }
}
