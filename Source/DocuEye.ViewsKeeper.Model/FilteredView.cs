using System.Collections.Generic;

namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Filtered view
    /// </summary>
    public class FilteredView : StaticDiagramView
    {
        /// <summary>
        /// The key of the view on which this filtered view is based.
        /// </summary>
        public string BaseViewKey { get; set; } = null!;
        /// <summary>
        /// Whether elements/relationships are being included or excluded based upon the set of tags.
        /// </summary>
        public string? Mode { get; set; }
        /// <summary>
        /// The set of tags to include/exclude elements/relationships when rendering this filtered view.
        /// </summary>
        public IEnumerable<string>? Tags { get; set; }
    }
}
