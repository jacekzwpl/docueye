using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Linter
{
    public static class LinterConfigurationExtensions
    {
        public static LinterConfiguration Merge(this LinterConfiguration target, LinterConfiguration source)
        {
            if (source == null)
            {
                return target;
            }
            // Merge MaxAllowedSeverity
            if (!string.IsNullOrEmpty(source.MaxAllowedSeverity))
            {
                target.MaxAllowedSeverity = source.MaxAllowedSeverity;
            }
            // Merge Rules
            //var newrules = new List<LinterRule>();
            foreach (var rule in source.Rules)
            {
                var targetRule = target.Rules.Where(o => o.Id == rule.Id).FirstOrDefault();
                if (targetRule != null)
                {
                    targetRule.Merge(rule);
                }
                else
                {
                    // Add new rule
                    target.Rules.Add(rule);
                }
            }

            // Merge Variables
            foreach (var kvp in source.Variables)
            {
                target.Variables[kvp.Key] = kvp.Value; // This will overwrite existing variables with the same key
            }
            return target;
        }
    }
}
