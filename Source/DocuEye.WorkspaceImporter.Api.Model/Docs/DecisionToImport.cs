using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Docs
{
    public class DecisionToImport
    {
        /// <summary>
        /// DocuEye ID of the documentation.
        /// </summary>
        public string DocumentationId { get; set; } = null!;
        /// <summary>
        /// The ID of the decision.
        /// </summary>
        public string? StrucuturizrId { get; set; }
        /// <summary>
        /// The date that the decision was made (ISO 8601 format).
        /// </summary>
        public string? Date { get; set; }
        /// <summary>
        /// The status of the decision.
        /// </summary>
        public string? Status { get; set; }
        /// <summary>
        /// The title of the decision.
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// The Markdown or AsciiDoc content of the section.
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// The content format type.
        /// </summary>
        public string? Format { get; set; }
        /// <summary>
        /// The ID of the element (in the model) that this decision applies to (optional).
        /// </summary>
        public string? StrucuturizrElementId { get; set; }
    }
}
