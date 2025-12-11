using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class WorkspaceAccessRuleToImportMap
    {
        public static WorkspaceAccessRule MapToWorkspaceAccessRule(this WorkspaceAccessRuleToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new WorkspaceAccessRule
            {
           
                Role = source.Role
            };
        }

        public static IEnumerable<WorkspaceAccessRule> MapToWorkspaceAccessRules(this IEnumerable<WorkspaceAccessRuleToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            foreach (var s in sources) yield return s.MapToWorkspaceAccessRule();
        }
    }
}
