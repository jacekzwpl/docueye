using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetSystemLandscapeView
{
    /// <summary>
    /// Handler for GetSystemLandscapeViewQuery
    /// </summary>
    public class GetSystemLandscapeViewQueryHandler : IQueryHandler<GetSystemLandscapeViewQuery, SystemLandscapeView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance 
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetSystemLandscapeViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>System Landscape View or null if no was found</returns>
        public async Task<SystemLandscapeView?> HandleAsync(GetSystemLandscapeViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.SystemLandscapeViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
