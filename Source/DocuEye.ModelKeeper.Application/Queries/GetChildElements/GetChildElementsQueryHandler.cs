using AutoMapper;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetChildElements
{
    /// <summary>
    /// Handler for GetChildElementsQuery
    /// </summary>
    public class GetChildElementsQueryHandler : IRequestHandler<GetChildElementsQuery, IEnumerable<ChildElement>>
    {
        private readonly IModelKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">Automapper service</param>
        public GetChildElementsQueryHandler(IModelKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of child elements</returns>
        public async Task<IEnumerable<ChildElement>> Handle(GetChildElementsQuery request, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(request.Type))
            {

                var elements = await this.dbContext.Elements
                    .Find(
                        o => o.Type == request.Type
                        && o.WorkspaceId == request.WorkspaceId
                        && o.ParentId == request.ParentId,
                        o => o.Name, true);
                return this.mapper.Map<IEnumerable<ChildElement>>(elements);
            }
            else
            {
                var elements = await this.dbContext.Elements
                    .Find(o => o.WorkspaceId == request.WorkspaceId && o.ParentId == request.ParentId, o => o.Name, true);
                return this.mapper.Map<IEnumerable<ChildElement>>(elements);
            }
        }
    }
}
