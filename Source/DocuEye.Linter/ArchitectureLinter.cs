using DocuEye.Linter.Model;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text.Json;

namespace DocuEye.Linter
{
    public class ArchitectureLinter
    {
        public LinterConfiguration Configuration { get; private set; } = new LinterConfiguration();

        private LinterModel model;

        public ArchitectureLinter(LinterModel model)
        {
            this.model = model;
        }

        public void LoadConfiguration(string json)
        {
            Configuration = JsonSerializer.Deserialize<LinterConfiguration>(json) ?? new LinterConfiguration();
            foreach (var rule in Configuration.Rules) {
                if (!new string[] { LinterRuleType.ModelElement, LinterRuleType.ModelRelationship }.Contains(rule.Type)) { 
                    throw new System.Exception($"Unsupported rule type: '{rule.Type}' for rule with key: '{rule.Key}'");
                }
                if (!new string[] { LinterRuleSeverity.Error, LinterRuleSeverity.Warning }.Contains(rule.Severity))
                {
                    throw new System.Exception($"Unsupported rule severity: '{rule.Severity}' for rule with key: '{rule.Key}'");
                }
            }
        }

        public IEnumerable<LinterIssue> Analyze()
        {
            List<LinterIssue> issues = new List<LinterIssue>();
            foreach (var rule in Configuration.Rules.Where(rule => rule.Enabled && rule.Type == "Element"))
            {
                var elements = model.Elements.AsQueryable().Where(rule.Expression).ToArray();
                if(elements.Count() > 0)
                {
                    issues.Add(new LinterIssue { Rule = rule });
                }
            }
            return issues;
        }
    }
}
