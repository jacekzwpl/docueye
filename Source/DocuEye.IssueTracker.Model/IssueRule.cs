using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.IssueTracker.Model
{
    public class IssueRule
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? HelpLink { get; set; }
    }
}
