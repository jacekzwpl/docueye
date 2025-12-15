using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Workspace
{
    public class WorkspaceAccessRuleToImport
    {
        /// <summary>
        /// Name of user role or user or user email
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Role in workspace
        /// </summary>
        public string Role { get; set; } = "ReadOnly";
    }
}
