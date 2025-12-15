using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonRelationshipView
    {
        /// <summary>
        /// The ID of the relationship.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// The description of this relationship (used in dynamic views only).
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Signifies whether this relationship represents a return/response message (used in dynamic views only).
        /// </summary>
        public bool? Response { get; set; }
        /// <summary>
        /// Gets the order of this relationship (used in dynamic views only; e.g. 1.0, 1.1, 2.0, etc).
        /// </summary>
        public string? Order { get; set; }
        /// <summary>
        /// The set of vertices used to render the relationship.
        /// </summary>
        public StructurizrJsonVertex? Vertices { get; set; }
        /// <summary>
        /// The routing algorithm used when rendering this individual relationship.
        /// </summary>
        public string? Routing { get; set; }
        /// <summary>
        /// The position of the annotation along the line; 0 (start) to 100 (end).
        /// </summary>
        public int? Position { get; set; }
        
    }
}
