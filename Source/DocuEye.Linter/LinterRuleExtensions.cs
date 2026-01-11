using DocuEye.Linter.Model;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace DocuEye.Linter
{
    public static class LinterRuleExtensions
    {
        public static IEnumerable<LinterIssue> Evaluate(this LinterRule rule, LinterModel model, List<object> evaluationContext, Dictionary<string,string> variablesMap)
        {
            var expression = rule.Expression;
            foreach (var item in variablesMap) {
                expression = expression.Replace(item.Key, item.Value);
            }

            if(!rule.Enabled)
            {
                return Enumerable.Empty<LinterIssue>();
            }

            var issues = new List<LinterIssue>();

            if (rule.Type == LinterRuleType.ModelElement) {
                var elements = model.Elements.AsQueryable()
                    .Where(expression, evaluationContext.ToArray()).ToArray();
                foreach (var element in elements)
                {
                    issues.Add(new LinterIssue { Rule = rule, Element = element });
                }
            }
            if(rule.Type == LinterRuleType.ModelRelationship)
            {
                var relationships = model.Relationships.AsQueryable()
                    .Where(expression, evaluationContext.ToArray()).ToArray();
                foreach (var relationship in relationships)
                {
                    issues.Add(new LinterIssue { Rule = rule, Relationship = relationship });
                }
            }

            if(rule.Type == LinterRuleType.General)
            {
                var results = new List<object>() { "general" };
                results = evaluationContext.AsQueryable()
                    .Where(expression, evaluationContext.ToArray()).ToList();
                if(results.Any())
                {
                    issues.Add(new LinterIssue { Rule = rule, Message = GeneralIssuesFinder.IssueMessage });
                }
            }

            return issues.ToArray();
        }
    }
}
