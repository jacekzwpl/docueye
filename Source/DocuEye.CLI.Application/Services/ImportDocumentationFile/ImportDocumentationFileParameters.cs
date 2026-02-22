using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.CLI.Application.Services.ImportDocumentationFile
{
    public class ImportDocumentationFileParameters
    {
        public string WorkspaceId { get; set; } = null!;
        public string? ElementId { get; set; }
        public string? ElementDslId { get; set; }
        public string DocumentationFile { get; set; } = null!;
        public string DocumentationFileType { get; set; } = null!;
    }
}
