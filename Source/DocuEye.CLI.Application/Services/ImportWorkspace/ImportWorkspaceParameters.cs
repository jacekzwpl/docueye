﻿namespace DocuEye.CLI.Application.Services.ImportWorkspace
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
        /// Path to workspace.json file, generated by structurizr cli.
        /// </summary>
        public string WorkspaceFilePath { get; set; }
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
        /// <param name="workspaceFilePath">Path to workspace.json file, generated by structurizr cli.</param>
        public ImportWorkspaceParameters(string importKey, string workspaceFilePath) { 
            this.ImportKey = importKey;
            this.WorkspaceFilePath = workspaceFilePath;
        }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="importKey">Unique import key.</param>
        /// <param name="workspaceFilePath">Path to workspace.json file, generated by structurizr cli.</param>
        /// <param name="workspaceId">The ID of the Workspace.</param>
        /// <param name="sourceLink">Link to source version from whitch workspace is imported</param>
        public ImportWorkspaceParameters(string importKey, string workspaceFilePath, string? workspaceId = null, string? sourceLink = null)
        {
            this.ImportKey = importKey;
            this.WorkspaceFilePath = workspaceFilePath;
            this.WorkspaceId = workspaceId;
            this.SourceLink = sourceLink;
        }
    }
}
