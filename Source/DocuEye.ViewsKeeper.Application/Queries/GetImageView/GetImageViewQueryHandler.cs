using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.GetImageView
{
    /// <summary>
    /// Handler for GetImageViewQuery
    /// </summary>
    public class GetImageViewQueryHandler : IRequestHandler<GetImageViewQuery, ImageView?>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance 
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetImageViewQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Image View or null if no was found</returns>
        public async Task<ImageView?> Handle(GetImageViewQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.ImageViews
                .FindOne(o => o.Id == request.Id && o.WorkspaceId == request.WorkspaceId);
        }
    }
}
