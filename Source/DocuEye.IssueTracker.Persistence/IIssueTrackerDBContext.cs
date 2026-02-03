using DocuEye.Infrastructure.MongoDB;
using DocuEye.IssueTracker.Model;
using System;
using System.Xml.Linq;

namespace DocuEye.IssueTracker.Persistence
{
    public interface IIssueTrackerDBContext
    {
        IGenericCollection<Issue> Issues { get; }
    }
}
