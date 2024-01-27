using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrComponent
    {
        /// <summary>
        /// The ID of this component in the model.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// The name of this component.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// A short description of this component.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The technology associated with this component (e.g. Apache Tomcat).
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// A comma separated list of tags associated with this component.
        /// </summary>
        public string? Tags { get; set; }
        /// <summary>
        /// The URL where more information about this element can be found.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// The name of the group in which this component should be included in.
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The set of relationships from this component to other elements.
        /// </summary>
        public IEnumerable<StructurizrRelationship>? Relationships { get; set; }
        /// <summary>
        /// Documentation for element
        /// </summary>
        public StructurizrDocumentation? Documentation { get; set; }
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
