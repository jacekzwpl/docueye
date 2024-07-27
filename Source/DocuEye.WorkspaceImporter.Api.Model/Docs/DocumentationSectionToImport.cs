using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Docs
{
    public class DocumentationSectionToImport
    {
        /// <summary>
        /// The Markdown or AsciiDoc content of the section.
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// The content format type.
        /// </summary>
        public string? Format { get; set; }
        /// <summary>
        /// The order (index) of the section in the document.
        /// </summary>
        public int? Order { get; set; }
    }
}
