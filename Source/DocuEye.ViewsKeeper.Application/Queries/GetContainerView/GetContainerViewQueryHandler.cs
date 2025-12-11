using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetContainerView
{
    /// <summary>
    /// Hander for GetContainerViewQuery
    /// </summary>
    public class GetContainerViewQueryHandler : IQueryHandler<GetContainerViewQuery, ContainerView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instancd
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetContainerViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Container view or null if no was found</returns>
        public async Task<ContainerView?> HandleAsync(GetContainerViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.ContainerViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
