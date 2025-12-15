using DocuEye.ModelKeeper.Persistence;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DocuEye.ModelKeeper.Model.Maps;
using DocuEye.ModelKeeper.Application.Model;
using DocuEye.Infrastructure.Mediator.Queries;

namespace DocuEye.ModelKeeper.Application.Queries.GetChildElements
{
    /// <summary>
    /// Handler for GetChildElementsQuery
    /// </summary>
    public class GetChildElementsQueryHandler : IQueryHandler<GetChildElementsQuery, IEnumerable<ChildElement>>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetChildElementsQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of child elements</returns>
        public async Task<IEnumerable<ChildElement>> HandleAsync(GetChildElementsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Type))
            {

                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Type == request.Type
                        && o.WorkspaceId == request.WorkspaceId
                        && o.ParentId == request.ParentId,
                        o => o.Name, true);
                return elements.MapToChildElements();
            }
            else
            {
                var elements = await this.dbContext.Elements
                    .Find(o => o.WorkspaceId == request.WorkspaceId && o.ParentId == request.ParentId, o => o.Name, true);
                return elements.MapToChildElements();
            }
        }
    }
}
