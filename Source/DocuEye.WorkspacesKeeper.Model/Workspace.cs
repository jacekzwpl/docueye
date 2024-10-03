using DocuEye.Infrastructure.Persistence.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspacesKeeper.Model
{
    /// <summary>
    /// Workspace 
    /// </summary>
    public class Workspace : BaseEntity
    {
        /// <summary>
        /// Workspace name
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Workspace description
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Workspace views list
        /// </summary>

        public IEnumerable<WorkspaceView> Views { get; set; } = Enumerable.Empty<WorkspaceView>();

        /// <summary>
        /// Workspace access rules
        /// </summary>
        public IEnumerable<WorkspaceAccessRule> AccessRules { get; set; } = Enumerable.Empty<WorkspaceAccessRule>();
        /// <summary>
        /// Defines visibility of workspace public/private
        /// </summary>
        public bool IsPrivate { get; set; } = false;
    }
}
