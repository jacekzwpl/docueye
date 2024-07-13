using AutoMapper;
using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Application.Queries.FindWorspaces
{
    /// <summary>
    /// Handler for FindWorkspacesQuery
    /// </summary>
    public class FindWorkspacesQueryHandler : IRequestHandler<FindWorkspacesQuery, IEnumerable<FoundedWorkspace>>
    {
        private readonly IWorkspacesKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">Mongo DB context</param>
        /// <param name="mapper">Automapper service</param>
        public FindWorkspacesQueryHandler(IWorkspacesKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of workspaces</returns>
        public async Task<IEnumerable<FoundedWorkspace>> Handle(FindWorkspacesQuery request, CancellationToken cancellationToken)
        {
            if(!string.IsNullOrEmpty(request.Name))
            {
                var worskpaces = await this.dbContext.Workspaces
                .Find(o => o.Name.ToLower().Contains(request.Name.ToLower()), o => o.Name, true, request.Limit, request.Skip);
                return this.mapper.Map<IEnumerable<FoundedWorkspace>>(worskpaces);
            }else
            {
                var worskpaces = await this.dbContext.Workspaces
                .Find(o => true, o => o.Name, true, request.Limit, request.Skip);
                return this.mapper.Map<IEnumerable<FoundedWorkspace>>(worskpaces);
            }
        }
    }
}
