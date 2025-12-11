using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.WorkspacesKeeper.Model;
using DocuEye.WorkspacesKeeper.Persistence;

using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace
{
    /// <summary>
    /// Handler for GetWorkspaceQuery
    /// </summary>
    public class GetWorkspaceQueryHandler : IQueryHandler<GetWorkspaceQuery, Workspace?>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetWorkspaceQueryHandler(IWorkspacesKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Workspace data or null if no was found</returns>
        public async Task<Workspace?> HandleAsync(GetWorkspaceQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Workspaces.FindOne(o => o.Id == request.Id);
        }
    }
}
