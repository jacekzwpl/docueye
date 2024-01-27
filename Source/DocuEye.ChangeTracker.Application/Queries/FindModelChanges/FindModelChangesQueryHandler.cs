using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DocuEye.ChangeTracker.Model;
using DocuEye.ChangeTracker.Persistence;

namespace DocuEye.ChangeTracker.Application.Queries.FindModelChanges
{
    /// <summary>
    /// Handles FindModelChangesQuery
    /// </summary>
    public class FindModelChangesQueryHandler : IRequestHandler<FindModelChangesQuery, IEnumerable<ModelChange>>
    {
        private readonly IChangeTrackerDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public FindModelChangesQueryHandler(IChangeTrackerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task<IEnumerable<ModelChange>> Handle(FindModelChangesQuery request, CancellationToken cancellationToken)
        {
            return await this.dbContext.ModelChanges
                .Find(o => o.WorkspaceId == request.WorkspaceId, o => o.EventTime, false, request.Limit, request.Skip);
        }
    }
}
