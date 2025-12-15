using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportClearDocItemsRequest
    {
        /// <summary>
        /// Workspace Identifier
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Unique identifier of current import
        /// </summary>
        public string ImportKey { get; set; } = null!;
    }
}
