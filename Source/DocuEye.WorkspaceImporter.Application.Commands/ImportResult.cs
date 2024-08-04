using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands
{
    public class ImportResult
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; protected set; }
        /// <summary>
        /// Indicates if import was finished with success
        /// </summary>
        public bool IsSuccess { get; protected set; }
        /// <summary>
        /// Import result message
        /// </summary>
        public string? Message { get; protected set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId"><Workspace ID/param>
        /// <param name="isSuccess">Indicates if import was finished with success</param>
        public ImportResult(string workspaceId, bool isSuccess)
        {
            this.WorkspaceId = workspaceId;
            this.IsSuccess = isSuccess;
        }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="isSuccess">Indicates if import was finished with success</param>
        /// <param name="message">Import result message</param>
        public ImportResult(string workspaceId, bool isSuccess, string message)
        {
            this.WorkspaceId = workspaceId;
            this.IsSuccess = isSuccess;
            this.Message = message;
        }
    }
}
