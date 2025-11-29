using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonElementView
    {
        /// <summary>
        /// The ID of the element.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// The horizontal position of the element when rendered.
        /// </summary>
        public int? X { get; set; }
        /// <summary>
        /// The vertical position of the element when rendered.
        /// </summary>
        public int? Y { get; set; }

    }
}
