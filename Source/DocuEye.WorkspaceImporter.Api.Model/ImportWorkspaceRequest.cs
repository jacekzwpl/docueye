using DocuEye.Structurizr.Model;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportWorkspaceRequest
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
        /// Workspace data to import
        /// </summary>
        public StructurizrWorkspace WorkspaceData { get; set; } = null!;
    }
}
