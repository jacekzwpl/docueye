﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrDynamicView
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
        /// The ID of the element this view is associated with (optional).
        /// </summary>
        public string? ElementId { get; set; }
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
        /// The set of elements in this views.
        /// </summary>
        public IEnumerable<StructurizrElementView>? Elements { get; set; }
        /// <summary>
        /// The set of relationships in this views.
        /// </summary>
        public IEnumerable<StructurizrRelationshipView>? Relationships { get; set; }
        /// <summary>
        /// Specifies software system/container boundaries should be visible for \"external\" containers/components (those outside the element in scope)
        /// </summary>
        public bool? ExternalBoundariesVisible { get; set; }
    }
}
