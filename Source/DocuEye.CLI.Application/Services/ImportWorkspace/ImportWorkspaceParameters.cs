using DocuEye.Structurizr.Json.Model;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    /// <summary>
    /// Import workspace parameters
    /// </summary>
    public class ImportWorkspaceParameters
    {
        /// <summary>
        /// Unique import key.
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// Worskspace Json Data
        /// </summary>
        public StructurizrJsonWorkspace WorkspaceData { get; set; }
        /// <summary>
        /// The ID of the Workspace.
        /// </summary>
        public string? WorkspaceId { get; set; }
        /// <summary>
        /// Link to source version from whitch workspace is imported
        /// </summary>
        public string? SourceLink { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="importKey">Unique import key.</param>
        /// <param name="workspaceData">Workspace Json Data</param>
        public ImportWorkspaceParameters(string importKey, StructurizrJsonWorkspace workspaceData) { 
            this.ImportKey = importKey;
            this.WorkspaceData = workspaceData;
        }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="importKey">Unique import key.</param>
        /// <param name="workspaceData">Workspace Json Data</param>
        /// <param name="workspaceId">The ID of the Workspace.</param>
        /// <param name="sourceLink">Link to source version from whitch workspace is imported</param>
        public ImportWorkspaceParameters(string importKey, StructurizrJsonWorkspace workspaceData, string? workspaceId = null, string? sourceLink = null)
        {
            this.ImportKey = importKey;
            this.WorkspaceData = workspaceData;
            this.WorkspaceId = workspaceId;
            this.SourceLink = sourceLink;
        }
    }
}
