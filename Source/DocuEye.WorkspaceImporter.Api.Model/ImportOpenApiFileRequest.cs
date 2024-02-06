using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportOpenApiFileRequest
    {
        public string Content { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
