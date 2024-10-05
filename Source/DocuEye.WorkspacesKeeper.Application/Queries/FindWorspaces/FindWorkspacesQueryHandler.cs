using AutoMapper;
using DocuEye.WorkspacesKeeper.Model;
using DocuEye.WorkspacesKeeper.Persistence;
using MediatR;
using MongoDB.Bson.Serialization.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
            Expression<Func<Workspace, bool>> filter;
            if (request.UserName != null && !string.IsNullOrEmpty(request.Name))
            {
                filter = o => o.Name.ToLower().Contains(request.Name.ToLower()) 
                && ((o.IsPrivate == false || o.IsPrivate == null) || (o.IsPrivate && o.AccessRules.Any(r => r.Name == request.UserName)));
            }else if(request.UserName != null && string.IsNullOrEmpty(request.Name))
            {
                filter = o => (o.IsPrivate == false || o.IsPrivate == null) || (o.IsPrivate && o.AccessRules.Any(r => r.Name == request.UserName));
            }else if(request.UserName == null && !string.IsNullOrEmpty(request.Name))
            {
                filter = o => o.Name.ToLower().Contains(request.Name.ToLower());
            }
            else {
                filter = o => true;
            }
            var worskpaces = await this.dbContext.Workspaces
                .Find(filter, o => o.Name, true, request.Limit, request.Skip);
            return this.mapper.Map<IEnumerable<FoundedWorkspace>>(worskpaces);
        }
    }
}
