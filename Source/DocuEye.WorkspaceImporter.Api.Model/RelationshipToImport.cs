using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model
{
    public class RelationshipToImport
    {
        /// <summary>
        /// The ID of relationship in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
        /// <summary>
        /// The ID of source element for this relationship in structurizr json
        /// </summary>
        public string StructurizrSourceId { get; set; } = null!;
        /// <summary>
        /// The ID of destination element for this realtionship in structurizr json
        /// </summary>
        public string StructurizrDestinationId { get; set; } = null!;
        /// <summary>
        /// The ID of linked relationship in structuriz json
        /// </summary>
        public string? StructurizrLinkedRelationshipId { get; set; }
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
        /// The technology associated with this relationship (e.g. HTTPS, JDBC, etc).
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// The interaction style (synchronous or asynchronous).
        /// </summary>
        public string? InteractionStyle { get; set; }
    }
}
