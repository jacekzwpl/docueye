using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Model
{
    public class StructurizrModel
    {
        public IEnumerable<StructurizrSoftwareSystem>? SoftwareSystems { get; set; }
        public IEnumerable<StructurizrPerson>? People { get; set; }  
        public IEnumerable<StructurizrDeploymentNode>? DeploymentNodes { get; set; }
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
