using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonTerminology
    {
        /// <summary>
        /// The terminology used when rendering the enterprise boundary.
        /// </summary>
        public string? Enterprise {  get; set; }
        /// <summary>
        /// The terminology used when rendering people.
        /// </summary>
        public string? Person { get; set; }
        /// <summary>
        /// The terminology used when rendering software systems.
        /// </summary>
        public string? SoftwareSystem { get; set; }
        /// <summary>
        /// The terminology used when rendering containers.
        /// </summary>
        public string? Container { get; set; }
        /// <summary>
        /// The terminology used when rendering components.
        /// </summary>
        public string? Component { get; set; }
        /// <summary>
        /// The terminology used when rendering code elements.
        /// </summary>
        public string? Code { get; set; }
        /// <summary>
        /// The terminology used when rendering deployment nodes.
        /// </summary>
        public string? DeploymentNode { get; set; }
        /// <summary>
        /// The terminology used when rendering relationships.
        /// </summary>
        public string? Relationship { get; set; }
        /// <summary>
        /// The terminology used when rendering infrastructure nodes.
        /// </summary>
        public string? InfrastructureNode { get; set; }
    }
}
