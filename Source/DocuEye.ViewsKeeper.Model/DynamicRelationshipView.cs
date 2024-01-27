namespace DocuEye.ViewsKeeper.Model
{
    /// <summary>
    /// Relationship of dynamic view
    /// </summary>
    public class DynamicRelationshipView
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
        /// The routing algorithm used when rendering this individual relationship.
        /// </summary>
        public string? Routing { get; set; }
        /// <summary>
        /// The position of the annotation along the line; 0 (start) to 100 (end).
        /// </summary>
        public int? Position { get; set; }
        /// <summary>
        /// The ID of source element taken from base relation
        /// </summary>
        public string? SourceId { get; set; }
        /// <summary>
        /// The ID of destination element taken from base relation
        /// </summary>
        public string? DestinationId { get; set; }
        /// <summary>
        /// The ID of relationship that dynamic relation is based on
        /// </summary>
        public string? BaseRelationshipId { get; set; }
    }
}
