using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    /// <summary>
    /// Request for importing documentation file for element in workspace
    /// </summary>
    public class ImportDocumentationFileRequest
    {
        /// <summary>
        /// Documentation file content
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Documentation file name
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Structurizr dsl element identifier - required when ElementId is null.
        /// </summary>
        public string? ElementDslId { get; set; }
        /// <summary>
        /// Element ID (DocuEye ID) - required when ElementDslId is null.
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// Type of documentation file - used to determine how documentation file will be processed and displayed in DocuEye.
        /// </summary>
        public string DocumentationType { get; set; } = null!;
    }
}
