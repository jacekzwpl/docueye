using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetDynamicView
{
    /// <summary>
    /// Handler for GetDynamicViewQuery
    /// </summary>
    public class GetDynamicViewQueryHandler : IQueryHandler<GetDynamicViewQuery, DynamicView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetDynamicViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Dynamic View or null if no was found</returns>
        public async Task<DynamicView?> HandleAsync(GetDynamicViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.DynamicViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
