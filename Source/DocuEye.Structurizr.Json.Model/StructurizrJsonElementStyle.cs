using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonElementStyle
    {
        /// <summary>
        /// The tag to which this element style applies.
        /// </summary>
        public string? Tag { get; set; }
        /// <summary>
        /// The width of the element, in pixels.
        /// </summary>
        public int? Width { get; set; }
        /// <summary>
        /// The height of the element, in pixels.
        /// </summary>
        public int? Height { get; set; }
        /// <summary>
        /// The background colour of the element, as a HTML RGB hex string (e.g. \'#ffffff\').
        /// </summary>
        public string? Background {  get; set; }
        /// <summary>
        /// The stroke colour of the element, as a HTML RGB hex string (e.g. \'#000000\').
        /// </summary>
        public string? Stroke { get; set;}
        /// <summary>
        /// The width of the stroke, in pixels.
        /// </summary>
        public string? StrokeWidth { get; set; }
        /// <summary>
        /// The foreground (text) colour of the element, as a HTML RGB hex string (e.g. \'#ffffff\').
        /// </summary>
        public string? Color { get; set; }
        /// <summary>
        /// The standard font size used to render text, in pixels.
        /// </summary>
        public int? FontSize { get; set; }
        /// <summary>
        /// The shape used to render the element.
        /// </summary>
        public string? Shape { get; set; }
        /// <summary>
        /// A Base64 data URI representation of a PNG/JPG/GIF file.
        /// </summary>
        public string? Icon { get; set; }
        /// <summary>
        /// The type of border used to render the element.
        /// </summary>
        public string? Border { get; set; }
        /// <summary>
        /// The opacity used when rendering the element; 0-100.
        /// </summary>
        public int? Opacity { get; set; }
        /// <summary>
        /// Whether the element metadata should be shown or not.
        /// </summary>
        public bool? Metadata { get; set; }
        /// <summary>
        /// Whether the element description should be shown or not.
        /// </summary>
        public bool? Description { get; set; }
 
    }
}
