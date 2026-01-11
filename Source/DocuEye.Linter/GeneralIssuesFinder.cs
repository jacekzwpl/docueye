using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core.CustomTypeProviders;

namespace DocuEye.Linter
{
    [DynamicLinqType]
    public static class GeneralIssuesFinder
    {
        public static string? IssueMessage;
        public static bool CyclicDependenciesExists(string elementType, IEnumerable<LinterModelRelationship> relationships)
        {
            IssueMessage = null;
            var result = ElementCycleDetector.CycleExists(elementType, relationships);

            if (result && ElementCycleDetector.CurrentCycle != null) {
                IssueMessage = "Cyclic dependencies discovered: " + string.Join(" -> ", ElementCycleDetector.CurrentCycle);
            }

            return result;
        }
    }
}
