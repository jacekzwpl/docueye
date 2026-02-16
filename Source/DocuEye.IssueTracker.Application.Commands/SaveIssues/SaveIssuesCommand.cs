using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.IssueTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.IssueTracker.Application.Commands.SaveIssues
{
    public class SaveIssuesCommand : ICommand
    {
        public string WorkspaceId { get; set; } = null!;
        public IEnumerable<Issue> Issues { get; set; } = Enumerable.Empty<Issue>();
    }
}
