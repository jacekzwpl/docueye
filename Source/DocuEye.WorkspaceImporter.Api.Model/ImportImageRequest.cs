using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class ImportImageRequest
    {
        public string WorkspaceId { get; set; } = null!;
        public string ImportKey { get; set; } = null!;
        public ImageToImport Image { get; set; } = null!;
    }
}
