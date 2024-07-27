using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Docs
{
    public class DocumentationToImport
    {
        public string Id { get; set; } = null!;
        public string? StructurizrElementId { get; set; }
        public IEnumerable<DocumentationSectionToImport> Sections { get; set; } = Enumerable.Empty<DocumentationSectionToImport>();
    }
}
