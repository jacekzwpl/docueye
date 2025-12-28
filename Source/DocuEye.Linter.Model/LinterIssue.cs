using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DocuEye.Linter.Model
{
    public class LinterIssue
    {
        public LinterRule Rule { get; set; } = null!;
        public LinterModelElement? Element { get; set; }
        public LinterModelRelationship? Relationship { get; set; }

        public int SeverityValue
        {
            get {
                return LinterRuleSeverity.GetSeverityValue(Rule.Severity);
            }
        }
    }
}
