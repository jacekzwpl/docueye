using DocuEye.Linter.Model;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DocuEye.Linter
{
    public static class LinterRuleExtensions
    {
        public static IEnumerable<LinterIssue> Evaluate(this LinterRule rule, LinterModel model)
        {
            if(!rule.Enabled)
            {
                return Enumerable.Empty<LinterIssue>();
            }

            var issues = new List<LinterIssue>();

            if (rule.Type == LinterRuleType.ModelElement) {
                var elements = model.Elements.AsQueryable().Where(rule.Expression).ToArray();
                foreach (var element in elements)
                {
                    issues.Add(new LinterIssue { Rule = rule, Element = element });
                }
            }
            if(rule.Type == LinterRuleType.ModelRelationship)
            {
                var relationships = model.Relationships.AsQueryable().Where(rule.Expression).ToArray();
                foreach (var relationship in relationships)
                {
                    issues.Add(new LinterIssue { Rule = rule, Relationship = relationship });
                }
            }

            return issues.ToArray();
        }
    }
}
