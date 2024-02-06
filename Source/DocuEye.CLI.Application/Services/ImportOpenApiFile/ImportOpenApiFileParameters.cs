using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.CLI.Application.Services.ImportOpenApiFile
{
    public class ImportOpenApiFileParameters
    {
        /// <summary>
        /// The ID of the Workspace.
        /// </summary>
        public string WorkspaceId { get; set; }
        public string ElementId { get; set; }
        public string OpenApiFile { get; set; }
    }
}
