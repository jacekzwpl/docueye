using DocuEye.Infrastructure.Persistence.Model;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Model
{
    /// <summary>
    /// Relationship
    /// </summary>
    public class Relationship : BaseEntity
    {
        /// <summary>
        /// A short description of this relationship.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// A comma separated list of tags associated with this relationship.
        /// </summary>
        public IEnumerable<string>? Tags { get; set; }
        /// <summary>
        /// The URL where more information about this relationship can be found.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The ID of the source element.
        /// </summary>
        public string SourceId { get; set; } = null!;
        /// <summary>
        /// The ID of the destination element.
        /// </summary>
        public string DestinationId { get; set; } = null!;
        /// <summary>
        /// The Name of the source element.
        /// </summary>
        //public string SourceName {  get; set; }
        /// <summary>
        /// The Name of the destination element.
        /// </summary>
        //public string DestinationName { get; set; }
        /// <summary>
        /// The technology associated with this relationship (e.g. HTTPS, JDBC, etc).
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// The interaction style (synchronous or asynchronous).
        /// </summary>
        public string? InteractionStyle { get; set; }
        /// <summary>
        /// The ID of the container-container relationship upon which this container instance-container instance relationship is based.
        /// </summary>
        public string? LinkedRelationshipId { get; set; }

        public string DslId { get; set; } = null!;
        /// <summary>
        /// The workspace id of this element 
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
    }
}
