using DocuEye.Infrastructure.Persistence.Model;
using System;

namespace DocuEye.IssueTracker.Model
{
    public class Issue : BaseEntity
    {
        public string WorkspaceId { get; set; } = null!;
        public int Severity { get; set; }
        public string? Message { get; set; } = null;
        public IssueRule Rule { get; set; } = null!;
        public IssueRelationship? Relationship { get; set; } = null;
        public IssueElement? Element { get; set; } = null;
    }
}
