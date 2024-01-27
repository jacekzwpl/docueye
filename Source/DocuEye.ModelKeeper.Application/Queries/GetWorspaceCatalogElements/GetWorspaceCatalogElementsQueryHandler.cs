using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetWorspaceCatalogElements
{
    /// <summary>
    /// Handler for GetWorspaceCatalogElementsQuery
    /// </summary>
    public class GetWorspaceCatalogElementsQueryHandler : IRequestHandler<GetWorspaceCatalogElementsQuery, IEnumerable<WorkspaceCatalogElement>>
    {
        private readonly IModelKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">Automapper service</param>
        public GetWorspaceCatalogElementsQueryHandler(IModelKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of elements</returns>
        public async Task<IEnumerable<WorkspaceCatalogElement>> Handle(GetWorspaceCatalogElementsQuery request, CancellationToken cancellationToken)
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
                return this.mapper.Map<IEnumerable<WorkspaceCatalogElement>>(elements);
            }
            else if (!string.IsNullOrEmpty(request.Name) && string.IsNullOrEmpty(request.Type))
            {
                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Name.ToLower().Contains(request.Name.ToLower())
                        && o.WorkspaceId == request.WorkspaceId
                        && !excludedTypes.Contains(o.Type),
                        o => o.Name, true, request.Limit, request.Skip);
                return this.mapper.Map<IEnumerable<WorkspaceCatalogElement>>(elements);
            }
            else if (string.IsNullOrEmpty(request.Name) && !string.IsNullOrEmpty(request.Type))
            {
                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Type == request.Type
                        && o.WorkspaceId == request.WorkspaceId
                        && !excludedTypes.Contains(o.Type),
                        o => o.Name, true, request.Limit, request.Skip);
                return this.mapper.Map<IEnumerable<WorkspaceCatalogElement>>(elements);
            }
            else
            {
                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.WorkspaceId == request.WorkspaceId 
                        && !excludedTypes.Contains(o.Type), 
                        o => o.Name, true, request.Limit, request.Skip);
                return this.mapper.Map<IEnumerable<WorkspaceCatalogElement>>(elements);
            }
        }
    }
}
