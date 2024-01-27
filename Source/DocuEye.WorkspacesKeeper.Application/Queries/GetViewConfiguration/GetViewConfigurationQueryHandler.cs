using DocuEye.WorkspacesKeeper.Model;
using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetViewConfiguration
{
    /// <summary>
    /// Handler for GetViewConfigurationQuery
    /// </summary>
    public class GetViewConfigurationQueryHandler : IRequestHandler<GetViewConfigurationQuery, ViewConfiguration?>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetViewConfigurationQueryHandler(IWorkspacesKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Workspace view configuration or null if no was found</returns>
        public async Task<ViewConfiguration?> Handle(GetViewConfigurationQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.ViewConfigurations.FindOne(o => o.Id == request.WorkspaceId);
        }
    }
}
