using DocuEye.DocsKeeper.Model;
using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.Queries.GetImage
{
    /// <summary>
    /// Handler for GetImageQuery
    /// </summary>
    public class GetImageQueryHandler : IQueryHandler<GetImageQuery, Image?>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetImageQueryHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>Image data or null if no was found</returns>
        public async Task<Image?> HandleAsync(GetImageQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Images
                .FindOne(o => o.DocumentationId == request.DocumentationId && o.Name == request.Name);
        }
    }
}
