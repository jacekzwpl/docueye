using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.CLI.ApiClient.Model
{
    public class ImportWorkspaceResult
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Indicates if import was finished with success
        /// </summary>
        public bool IsSuccess { get; set; }
        /// <summary>
        /// Import result message
        /// </summary>
        public string? Message { get; set; }
    }
}
