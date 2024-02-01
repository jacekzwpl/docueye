using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.CLI.Application.Services.ImportWorkspace
{
    public class ImportWorkspaceParameters
    {
        public string ImportKey { get; set; }
        public string WorkspaceFilePath { get; set; }
        public string? WorkspaceId { get; set; } 
        public string? SourceLink { get; set; }

        public ImportWorkspaceParameters(string importKey, string workspaceFilePath) { 
            this.ImportKey = importKey;
            this.WorkspaceFilePath = workspaceFilePath;
        }

        public ImportWorkspaceParameters(string importKey, string workspaceFilePath, string? workspaceId = null, string? sourceLink = null)
        {
            this.ImportKey = importKey;
            this.WorkspaceFilePath = workspaceFilePath;
            this.WorkspaceId = workspaceId;
            this.SourceLink = sourceLink;
        }
    }
}
