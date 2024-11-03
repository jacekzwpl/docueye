using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportWorkspaceStartRequest
    {
        /// <summary>
        /// Workspace Identifier
        /// </summary>
        public string? WorkspaceId { get; set; }
        /// <summary>
        /// Unique identifier of current import
        /// </summary>
        public string ImportKey { get; set; } = null!;
        /// <summary>
        /// Link to source version form which worskpace is imported
        /// </summary>
        public string? SourceLink { get; set; }
        /// <summary>
        /// Workspace name
        /// </summary>
        public string WorkspaceName { get; set; } = null!;
        /// <summary>
        /// Workspace description
        /// </summary>
        public string? WorkspaceDescription { get; set; }

        public string? Visibility { get; set; }

        public IEnumerable<WorkspaceAccessRuleToImport>? AccessRules { get; set; }
    }
}
