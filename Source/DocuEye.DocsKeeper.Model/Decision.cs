using DocuEye.Infrastructure.Persistence.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.DocsKeeper.Model
{
    public class Decision : BaseEntity
    {
        /// <summary>
        /// The date that the decision was made (ISO 8601 format).
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// The status of the decision.
        /// </summary>
        public string Status { get; set; } = null!;
        /// <summary>
        /// The title of the decision.
        /// </summary>
        public string Title { get; set; } = null!;
        /// <summary>
        /// The Markdown or AsciiDoc content of the section.
        /// </summary>
        public string Content { get; set; } = string.Empty;
        /// <summary>
        /// The content format type.
        /// </summary>
        public string Format { get; set; } = null!;
        /// <summary>
        /// The ID of the element (in the model) that this decision applies to (optional).
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// Decision links
        /// </summary>
        public IEnumerable<DecisionLink> Links { get; set; } = Enumerable.Empty<DecisionLink>();
        /// <summary>
        /// Id of documentation that this decision belongs to
        /// </summary>
        public string DocumentationId { get; set; } = null!;
        /// <summary>
        /// Id of workspace that this decision belongs to
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Structurizr dsl ID
        /// </summary>
        public string DslId { get; set; } = null!;
        /// <summary>
        /// Import key that last update decision
        /// </summary>
        public string ImportKey { get; set; } = null!;
    }
}
