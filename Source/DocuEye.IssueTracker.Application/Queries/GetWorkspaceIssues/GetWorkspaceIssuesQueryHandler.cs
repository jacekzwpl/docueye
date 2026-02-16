using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.IssueTracker.Model;
using DocuEye.IssueTracker.Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.IssueTracker.Application.Queries.GetWorkspaceIssues
{
    public class GetWorkspaceIssuesQueryHandler : IQueryHandler<GetWorkspaceIssuesQuery, IEnumerable<Issue>>
    {
        private readonly IIssueTrackerDBContext dbContext;

        public GetWorkspaceIssuesQueryHandler(IIssueTrackerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Issue>> HandleAsync(GetWorkspaceIssuesQuery query, CancellationToken cancellationToken = default)
        {
            return await this.dbContext.Issues.Find(o => o.WorkspaceId == query.WorkspaceId);
        }
    }
}
