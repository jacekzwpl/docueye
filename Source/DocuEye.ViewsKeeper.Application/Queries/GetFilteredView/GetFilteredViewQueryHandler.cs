using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetFilteredView
{
    /// <summary>
    /// Handler for GetFilteredViewQuery
    /// </summary>
    public class GetFilteredViewQueryHandler : IQueryHandler<GetFilteredViewQuery, FilteredView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance 
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetFilteredViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Filtered View or null if no was found</returns>
        public async Task<FilteredView?> HandleAsync(GetFilteredViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.FilteredViews
                .FindOne(o => o.Id == request.Id);
        }
    }
}
