using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetAllWorkspaceElements
{
    /// <summary>
    /// Handler for GetAllWorkspaceElementsQuery
    /// </summary>
    public class GetAllWorkspaceElementsQueryHandler : IRequestHandler<GetAllWorkspaceElementsQuery, IEnumerable<Element>>
    {
        private readonly IModelKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public GetAllWorkspaceElementsQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of elements</returns>
        public async Task<IEnumerable<Element>> Handle(GetAllWorkspaceElementsQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.Elements
                .Find(o => o.WorkspaceId == request.WorkspaceId);
            
        }
    }
}
