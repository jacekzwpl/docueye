using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterConfiguration
    {
        public string? Extends { get; set; }
        public string MaxAllowedSeverity { get; set; } = LinterRuleSeverity.Warning;
        public List<LinterRule> Rules { get; set; } = new List<LinterRule>();
        public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
    }
}
