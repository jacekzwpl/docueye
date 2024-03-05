using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspacesKeeper.Model
{
    /// <summary>
    /// Workspsace Access rule, defines who has access to workspace.
    /// </summary>
    public class WorkspaceAccessRule
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
