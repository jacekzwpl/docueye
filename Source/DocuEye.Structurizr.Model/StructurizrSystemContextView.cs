using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrSystemContextView
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
        /// The ID of the software system this view is associated with.
        /// </summary>
        public string? SoftwareSystemId { get; set; }
        /// <summary>
        /// The paper size that should be used to render this view.
        /// </summary>
        public string? PaperSize { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public StructurizrDimensions? Dimensions { get; set; }
        ///
        public StructurizrAutomaticLayout? AutomaticLayout { get; set; }
        /// <summary>
        /// Specifies whether the enterprise boundary (to differentiate internal elements from external elements) should be visible on the resulting diagram.
        /// </summary>
        public bool? EnterpriseBoundaryVisible { get; set; }
        /// <summary>
        /// The set of elements in this views.
        /// </summary>
        public IEnumerable<StructurizrElementView>? Elements { get; set; }
        /// <summary>
        /// The set of relationships in this views.
        /// </summary>
        public IEnumerable<StructurizrRelationshipView>? Relationships { get; set; }
        /// <summary>
        /// The set of animation steps (optional).
        /// </summary>
        public IEnumerable<StructurizrAnimationStep>? Animations { get; set; }
    }
}
