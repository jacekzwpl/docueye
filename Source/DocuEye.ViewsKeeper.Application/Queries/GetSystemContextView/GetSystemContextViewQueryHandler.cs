using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetSystemContextView
{
    /// <summary>
    /// Handler for GetSystemContextViewQuery
    /// </summary>
    public class GetSystemContextViewQueryHandler : IRequestHandler<GetSystemContextViewQuery, SystemContextView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance 
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetSystemContextViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>System Context View or null if no was found</returns>
        public async Task<SystemContextView?> Handle(GetSystemContextViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.SystemContextViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
