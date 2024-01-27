using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrRelationshipStyle
    {
        /// <summary>
        /// The tag to which this relationship style applies.
        /// </summary>
        public string? Tag { get; set; }
        /// <summary>
        /// The thickness of the line, in pixels.
        /// </summary>
        public int? Thickness { get; set; }
        /// <summary>
        /// The colour of the line, as a HTML RGB hex string (e.g. \'#ffffff\').
        /// </summary>
        public string? Color { get; set; }
        /// <summary>
        /// The standard font size used to render the relationship annotation, in pixels.
        /// </summary>
        public int? FontSize { get; set; }

        /// <summary>
        /// The width of the relationship annotation, in pixels.
        /// </summary>
        public int? Width { get; set; }
        /// <summary>
        /// A flag to indicate whether the line is rendered as dashed or not.
        /// </summary>
        public bool? Dashed { get; set; }
        /// <summary>
        /// The routing algorithm used when rendering lines.
        /// </summary>
        public string? Routing { get; set; }
        /// <summary>
        /// The position of the annotation along the line; 0 (start) to 100 (end).
        /// </summary>
        public int? Position { get; set; }
        /// <summary>
        /// The opacity used when rendering the line; 0-100.
        /// </summary>
        public int? Opacity { get; set; }
    }
}
