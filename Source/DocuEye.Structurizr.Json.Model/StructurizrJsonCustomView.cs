using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonCustomView
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
        /// The paper size that should be used to render this view.
        /// </summary>
        public string? PaperSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public StructurizrJsonDimensions? Dimensions { get; set; }
        ///
        public StructurizrJsonAutomaticLayout? AutomaticLayout { get; set; }
        /// <summary>
        /// The set of elements in this views.
        /// </summary>
        public IEnumerable<StructurizrJsonElementView>? Elements { get; set; }
        /// <summary>
        /// The set of relationships in this views.
        /// </summary>
        public IEnumerable<StructurizrJsonRelationshipView>? Relationships { get; set; }
        /// <summary>
        /// The set of animation steps (optional).
        /// </summary>
        public IEnumerable<StructurizrJsonAnimationStep>? Animations { get; set; }
    }
}
