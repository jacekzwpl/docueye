using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportWorkspace
{
    /// <summary>
    /// Import workspace command result
    /// </summary>
    public class ImportWorkspaceResult
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Indicates if import was finished with success
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Import result message
        /// </summary>
        public string? Message { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId"><Workspace ID/param>
        /// <param name="isSuccess">Indicates if import was finished with success</param>
        public ImportWorkspaceResult(string workspaceId, bool isSuccess) { 
            this.WorkspaceId = workspaceId;
            this.IsSuccess = isSuccess;
        }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="isSuccess">Indicates if import was finished with success</param>
        /// <param name="message">Import result message</param>
        public ImportWorkspaceResult(string workspaceId, bool isSuccess, string message) {
            this.WorkspaceId = workspaceId;
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}
