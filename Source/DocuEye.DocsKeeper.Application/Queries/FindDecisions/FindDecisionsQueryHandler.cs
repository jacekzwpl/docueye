using AutoMapper;
using DocuEye.DocsKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.FindDecisions
{
    /// <summary>
    /// Handler for FindDecisionsQuery
    /// </summary>
    public class FindDecisionsQueryHandler : IRequestHandler<FindDecisionsQuery, IEnumerable<FoundedDecision>>
    {
        private readonly IDocsKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">IMapper service</param>
        public FindDecisionsQueryHandler(IDocsKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
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
            return this.mapper.Map<IEnumerable<FoundedDecision>>(decisions);
            
        }
    }
}
