using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{

    /// <summary>
    /// Request for importing openapi definition for element in workspace
    /// </summary>
    public class ImportOpenApiFileRequest
    {
        /// <summary>
        /// Open Api definition content
        /// </summary>
        public string Content { get; set; } = null!;
        /// <summary>
        /// Openapi definition file name
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
    }
}
