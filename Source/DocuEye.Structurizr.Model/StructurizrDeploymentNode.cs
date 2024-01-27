using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrDeploymentNode
    {
        /// <summary>
        /// The ID of this deployment node in the model.
        /// </summary>
        public string? Id { get; set; }
        /// <summary>
        /// The name of this deployment node.
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// A short description of this deployment node.
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// The technology associated with this deployment node (e.g. Apache Tomcat).
        /// </summary>
        public string? Technology { get; set; }
        /// <summary>
        /// The deployment environment in which this deployment node resides (e.g. \"Development\", \"Live\", etc).
        /// </summary>
        public string? Environment { get; set; }
        /// <summary>
        /// The number of instances; either a number (e.g. 1, 2, etc) or a range (e.g. 0..N, 0..*, 1..3, etc).
        /// </summary>
        public string? Instances { get; set; }
        /// <summary>
        /// A comma separated list of tags associated with this deployment node.
        /// </summary>
        public string? Tags { get; set; }
        /// <summary>
        /// The URL where more information about this element can be found.
        /// </summary>
        public string? Url { get; set; }
        /// <summary>
        /// The name of the group in which this deployment node should be included in.
        /// </summary>
        public string? Group { get; set; }
        /// <summary>
        /// The set of child/nested deployment nodes.
        /// </summary>
        public IEnumerable<StructurizrDeploymentNode>? Children { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The set of container instances running in this deployment node..
        /// </summary>
        public IEnumerable<StructurizrContainerInstance>? ContainerInstances { get; set; }
        /// <summary>
        /// The set of software systems instances running in this deployment node..
        /// </summary>
        public IEnumerable<StructurizrSoftwareSystemInstance>? SoftwareSystemInstances { get; set; }
        /// <summary>
        /// The set of relationships from this deployment node to other elements.
        /// </summary>
        public IEnumerable<StructurizrRelationship>? Relationships { get; set; }
        /// <summary>
        /// The set of infrastructure nodes running in this deployment node..
        /// </summary>
        public IEnumerable<StructurizrInfrastructureNode>? InfrastructureNodes { get; set; }

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
