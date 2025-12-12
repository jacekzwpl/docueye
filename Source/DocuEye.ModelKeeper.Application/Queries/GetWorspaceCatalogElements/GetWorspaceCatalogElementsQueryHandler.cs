using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Application.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Model.Maps;
using DocuEye.ModelKeeper.Persistence;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DocuEye.ModelKeeper.Application.Queries.GetWorspaceCatalogElements
{
    /// <summary>
    /// Handler for GetWorspaceCatalogElementsQuery
    /// </summary>
    public class GetWorspaceCatalogElementsQueryHandler : IQueryHandler<GetWorspaceCatalogElementsQuery, IEnumerable<WorkspaceCatalogElement>>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetWorspaceCatalogElementsQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of elements</returns>
        public async Task<IEnumerable<WorkspaceCatalogElement>> HandleAsync(GetWorspaceCatalogElementsQuery request, CancellationToken cancellationToken)
        {
            var excludedTypes = new List<string>()
            {
                ElementType.SoftwareSystemInstance,
                ElementType.ContainerInstance
            };

            if (!string.IsNullOrEmpty(request.Name) && !string.IsNullOrEmpty(request.Type))
            {

                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Name.ToLower().Contains(request.Name.ToLower())
                        && o.Type == request.Type
                        && o.WorkspaceId == request.WorkspaceId
                        && !excludedTypes.Contains(o.Type),
                        o => o.Name, true, request.Limit, request.Skip);
                return elements.MapToWorkspaceCatalogElements();
            }
            else if (!string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Type))
            {
                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Name.ToLower().Contains(request.Name.ToLower())
                        && o.WorkspaceId == request.WorkspaceId
                        && !excludedTypes.Contains(o.Type),
                        o => o.Name, true, request.Limit, request.Skip);
                return elements.MapToWorkspaceCatalogElements();
            }
            else if (string.IsNullOrEmpty(request.Name) && !string.IsNullOrEmpty(request.Type))
            {
                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Type == request.Type
                        && o.WorkspaceId == request.WorkspaceId
                        && !excludedTypes.Contains(o.Type),
                        o => o.Name, true, request.Limit, request.Skip);
                return elements.MapToWorkspaceCatalogElements();
            }
            else
            {
                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.WorkspaceId == request.WorkspaceId 
                        && !excludedTypes.Contains(o.Type), 
                        o => o.Name, true, request.Limit, request.Skip);
                return elements.MapToWorkspaceCatalogElements();
            }
        }
    }
}
