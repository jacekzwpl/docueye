using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.IssueTracker.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.IssueTracker.Application.Queries.GetWorkspaceIssues
{
    public class GetWorkspaceIssuesQuery : IQuery<IEnumerable<Issue>>
    {
        public string WorkspaceId { get; set; }
        public GetWorkspaceIssuesQuery(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
