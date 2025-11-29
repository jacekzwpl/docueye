using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    /// <summary>
    /// An infrastructure node.
    /// </summary>
    public class StructurizrJsonInfrastructureNode
    {
        /// <summary>
        /// The ID of this infrastructure node in the model.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// The name of this infrastructure node.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// A short description of this infrastructure node.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The technology associated with this infrastructure node (e.g. \"Route 53\").
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// The deployment environment in which this infrastructure node resides (e.g. \"Development\", \"Live\", etc).
        /// </summary>
        public string? Environment { get; set; }
        /// <summary>
        /// A comma separated list of tags associated with this infrastructure node.
        /// </summary>
        public string? Tags { get; set; }
        /// <summary>
        /// The URL where more information about this element can be found.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// The name of the group in which this infrastructure node should be included in.
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The set of relationships from this infrastructure node to other elements.
        /// </summary>
        public IEnumerable<StructurizrJsonRelationship>? Relationships { get; set; }
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

        public string? OwnerTeam
        {
            get
            {
                if (this.Properties.ContainsKey(DslPropertyNames.OwnerTeam))
                {
                    return this.Properties[DslPropertyNames.OwnerTeam];
                }
                return null;
            }
        }
        public string? SourceCodeUrl
        {
            get
            {
                if (this.Properties.ContainsKey(DslPropertyNames.SourceCodeUrl))
                {
                    return this.Properties[DslPropertyNames.SourceCodeUrl];
                }
                return null;
            }
        }
    }
}
