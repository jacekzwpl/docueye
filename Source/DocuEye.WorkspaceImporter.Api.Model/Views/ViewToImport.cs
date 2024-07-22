using System.Collections.Generic;
using System.Linq;

namespace DocuEye.WorkspaceImporter.Api.Model.Views
{
    public class ViewToImport
    {

        /// <summary>
        /// The View Type
        /// </summary>
        public string ViewType { get; set; } = string.Empty;

        /// <summary>
        /// The title of this view (optional).
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// The description of this view.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// A unique identifier for this view.
        /// </summary>
        public string? Key { get; set; }
        /// <summary>
        /// The paper size that should be used to render this view.
        /// </summary>
        public string? PaperSize { get; set; }
        /// <summary>
        /// The set of elements in this views.
        /// </summary>
        public IEnumerable<ElementInView> Elements { get; set; } = Enumerable.Empty<ElementInView>();
        /// <summary>
        /// The set of relationships in this views.
        /// </summary>
        public IEnumerable<RelationshipInView> Relationships { get; set; } = Enumerable.Empty<RelationshipInView>();
        /// <summary>
        /// Automatic layout configuration
        /// </summary>
        //public AutomaticLayout? AutomaticLayout { get; set; }
        /// <summary>
        /// The ID of the element this view is associated with (optional).
        /// </summary>
        public string? StructurizrElementId { get; set; }
        /// <summary>
        /// Specifies software system/container boundaries should be visible for \"external\" containers/components (those outside the element in scope)
        /// </summary>
        public bool? ExternalBoundariesVisible { get; set; }

        #region ImageView

        /// <summary>
        /// Image View Content
        /// </summary>
        public string? Content { get; set; }
        /// <summary>
        /// Image View Content Type
        /// </summary>
        public string? ContentType { get; set; }

        #endregion

        #region FilteredView

        /// <summary>
        /// The key of the view on which this filtered view is based.
        /// </summary>
        public string? BaseViewKey { get; set; }
        /// <summary>
        /// Whether elements/relationships are being included or excluded based upon the set of tags.
        /// </summary>
        public string? Mode { get; set; }
        /// <summary>
        /// The set of tags to include/exclude elements/relationships when rendering this filtered view.
        /// </summary>
        public IEnumerable<string>? Tags { get; set; }

        #endregion


    }
}
