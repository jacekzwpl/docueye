using DocuEye.Infrastructure.Persistence.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.DocsKeeper.Model
{
    /// <summary>
    /// Documentation
    /// </summary>
    public class Documentation : BaseEntity
    {
        /// <summary>
        /// The ID of workspace that this documentation belongs to
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// The ID of the element that this documentation belongs to
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// The sections of documentation
        /// </summary>
        public IEnumerable<DocumentationSection> Sections { get; set; } = Enumerable.Empty<DocumentationSection>();
    }
}
