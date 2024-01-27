using DocuEye.Infrastructure.Persistence.Model;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspacesKeeper.Model
{
    /// <summary>
    /// Views configuration
    /// </summary>
    public class ViewConfiguration : BaseEntity
    {
        /// <summary>
        /// Set of styles for elements in view
        /// </summary>
        public IEnumerable<ElementStyle> ElementStyles { get; set; } = Enumerable.Empty<ElementStyle>();
        public IEnumerable<RelationshipStyle> RelationshipStyles { get; set; } = Enumerable.Empty<RelationshipStyle>();
        /// <summary>
        /// Themes url list
        /// </summary>
        public IEnumerable<string>? Themes { get; set; }
        /// <summary>
        /// Terminology
        /// </summary>
        public Terminology? Terminology { get; set; }
        /// <summary>
        /// Group separator
        /// </summary>
        public string? GroupSeparator { get; set; } = "|";
    }
}
