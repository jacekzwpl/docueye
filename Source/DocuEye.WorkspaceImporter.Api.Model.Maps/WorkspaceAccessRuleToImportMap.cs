using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using DocuEye.WorkspacesKeeper.Model;
using System;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class WorkspaceAccessRuleToImportMap
    {
        public static WorkspaceAccessRule ToWorkspaceAccessRule(this WorkspaceAccessRuleToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new WorkspaceAccessRule
            {
           
                Role = source.Role
            };
        }
    }
}
