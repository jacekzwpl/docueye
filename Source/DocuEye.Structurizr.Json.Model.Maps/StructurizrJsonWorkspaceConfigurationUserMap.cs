using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonWorkspaceConfigurationUserMap
    {
        public static WorkspaceAccessRuleToImport ToWorkspaceAccessRuleToImport(this StructurizrJsonWorkspaceConfigurationUser source) {
            if (source == null) throw new ArgumentNullException(nameof(source));

            return new WorkspaceAccessRuleToImport
            {
                Name = source.Username,
                Role = source.Role
            };
        }

        public static IEnumerable<WorkspaceAccessRuleToImport> ToWorkspaceAccessRuleToImport(this IEnumerable<StructurizrJsonWorkspaceConfigurationUser>? source) {
            if (source == null) throw new ArgumentNullException(nameof(source));
            var result = new List<WorkspaceAccessRuleToImport>();
            foreach (var item in source)
            {
                result.Add(item.ToWorkspaceAccessRuleToImport());
            }
            return result.AsEnumerable();
        }
    }
}
