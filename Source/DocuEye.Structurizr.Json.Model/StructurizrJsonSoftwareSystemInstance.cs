using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonSoftwareSystemInstance
    {
        /// <summary>
        /// The ID of this software system instance in the model.
        /// </summary>
        public string Id { get; set; } = null!;
        /// <summary>
        /// The ID of the software system this is an instance of.
        /// </summary>
        public string SoftwareSystemId { get; set; } = null!;
        /// <summary>
        /// The number/index of this instance.
        /// </summary>
        public int? InstanceId { get; set; }
        /// <summary>
        /// The deployment environment in which this software system instance resides (e.g. \"Development\", \"Live\", etc).
        /// </summary>
        public string? Environment { get; set; }
        /// <summary>
        /// A comma separated list of tags associated with this software system instance.
        /// </summary>
        public string? Tags { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// The set of relationships from this software system instance to other elements.
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
        
        ///**
        // * The set of HTTP-based health checks for this software system instance.
        // * @type {Array<StructurizrHttpHealthCheck>}
        // * @memberof StructurizrSoftwareSystemInstance
        // */
        //'healthChecks'?: Array<StructurizrHttpHealthCheck>;
    }
}
