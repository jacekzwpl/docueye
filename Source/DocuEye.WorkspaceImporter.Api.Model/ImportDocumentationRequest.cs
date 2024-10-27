using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportDocumentationRequest
    {
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
        public DocumentationToImport Documentation { get; set; } = null!;
    }
}
