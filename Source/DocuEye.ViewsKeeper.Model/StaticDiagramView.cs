using System.Collections.Generic;
using System.Linq;

namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Static diagram view
    /// </summary>
    public abstract class StaticDiagramView : BaseView
    {
        /// <summary>
        /// The paper size that should be used to render this view.
        /// </summary>
        public string? PaperSize { get; set; }
        /// <summary>
        /// The set of elements in this views.
        /// </summary>
        public IEnumerable<ElementView> Elements { get; set; } = Enumerable.Empty<ElementView>();
        /// <summary>
        /// The set of relationships in this views.
        /// </summary>
        public IEnumerable<RelationshipView> Relationships { get; set; } = Enumerable.Empty<RelationshipView>();
        /// <summary>
        /// Automatic layout configuration
        /// </summary>
        public AutomaticLayout? AutomaticLayout { get; set; }
    }
}
