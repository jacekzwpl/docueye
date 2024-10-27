using System;
using System.Collections.Generic;
using System.Text;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportElementsRequest
    {
        public IEnumerable<ElementToImport> Elements { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public string WorkspaceId { get; set; } = null!;
    }
}
