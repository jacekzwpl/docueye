using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetDeploymentView
{
    /// <summary>
    /// Handler for GetDeploymentViewQuery
    /// </summary>
    public class GetDeploymentViewQueryHandler : IQueryHandler<GetDeploymentViewQuery, DeploymentView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetDeploymentViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Deployment View or null if no was found</returns>
        public async Task<DeploymentView?> HandleAsync(GetDeploymentViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.DeploymentViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
