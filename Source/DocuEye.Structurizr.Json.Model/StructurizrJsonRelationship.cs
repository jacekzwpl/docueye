using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonRelationship
    {

        /// <summary>
        /// The ID of this relationship in the model.
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// A short description of this relationship.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// A comma separated list of tags associated with this relationship.
        /// </summary>
        public string? Tags { get; set; }
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
        public string DestinationId {  get; set; } = null!;
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


        /// <summary>
        /// Structurizr DSL Identiier
        /// </summary>
        public string DslId
        {
            get
            {
                if (this.Properties.ContainsKey(DslPropertyNames.DslIdProperty))
                {
                    return this.Properties[DslPropertyNames.DslIdProperty];
                }
                return string.Empty;
            }
        }

    }
}
