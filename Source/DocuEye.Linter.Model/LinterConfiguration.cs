using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterConfiguration
    {

        public string MaxAllowedSeverity { get; set; } = LinterRuleSeverity.Warning;
        public IEnumerable<LinterRule> Rules { get; set; } = Enumerable.Empty<LinterRule>();
        public Dictionary<string, object> Variables { get; set; } = new Dictionary<string, object>();
    }
}
