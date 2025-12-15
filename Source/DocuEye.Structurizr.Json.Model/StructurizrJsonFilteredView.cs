using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonFilteredView
    {
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
    }
}
