using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model
{
    public class StructurizrJsonModel
    {
        public IEnumerable<StructurizrJsonSoftwareSystem>? SoftwareSystems { get; set; }
        public IEnumerable<StructurizrJsonPerson>? People { get; set; }  
        public IEnumerable<StructurizrJsonDeploymentNode>? DeploymentNodes { get; set; }

        public IEnumerable<StructurizrJsonCustomElement>? CustomElements { get; set; }
        /// <summary>
        /// A set of arbitrary name-value properties.
        /// </summary>
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();


        public string GroupSeparator
        {
            get {
                if (this.Properties.ContainsKey(DslPropertyNames.GroupSeparator))
                {
                    return this.Properties[DslPropertyNames.GroupSeparator];
                }
                return "|";
            }
        }
    }
}
