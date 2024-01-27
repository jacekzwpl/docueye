using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetComponentView
{
    /// <summary>
    /// Handler for GetComponentViewQuery
    /// </summary>
    public class GetComponentViewQueryHandler : IRequestHandler<GetComponentViewQuery, ComponentView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">mongoDB context</param>
        public GetComponentViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Componet view or null if no was found</returns>
        public async Task<ComponentView?> Handle(GetComponentViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.ComponentViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
