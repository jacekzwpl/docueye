using System.Collections.Generic;
using System.Linq;

namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Dynamic view
    /// </summary>
    public class DynamicView : BaseView
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
        public IEnumerable<DynamicRelationshipView> Relationships { get; set; } = Enumerable.Empty<DynamicRelationshipView>();
        /// <summary>
        /// 
        /// </summary>
        public AutomaticLayout? AutomaticLayout { get; set; }
        /// <summary>
        /// The ID of the element this view is associated with (optional).
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// Specifies software system/container boundaries should be visible for \"external\" containers/components (those outside the element in scope)
        /// </summary>
        public bool? ExternalBoundariesVisible { get; set; }
    }
}
