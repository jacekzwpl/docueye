using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocuEye.DocsKeeper.Model.Maps;
using DocuEye.DocsKeeper.Application.Model;

namespace DocuEye.DocsKeeper.Application.Queries.FindDecisions
{
    /// <summary>
    /// Handler for FindDecisionsQuery
    /// </summary>
    public class FindDecisionsQueryHandler : IRequestHandler<FindDecisionsQuery, IEnumerable<FoundedDecision>>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">IMapper service</param>
        public FindDecisionsQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of decisions</returns>
        public async Task<IEnumerable<FoundedDecision>> Handle(FindDecisionsQuery request, CancellationToken cancellationToken)
        {

            var decisions = await this.dbContext.Decisions
                .Find(
                    o => o.WorkspaceId == request.WorkspaceId && o.ElementId == request.ElementId, 
                    o => o.Date, false, request.Limit, request.Skip);
            return decisions.MapToFoundedDecisions();//this.mapper.Map<IEnumerable<FoundedDecision>>(decisions);


        }
    }
}
