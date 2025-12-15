using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrModelElement
    {
        public string ModelId { get; set; }
        public string Identifier { get; set; }
        public string? ParentIdentifier { get; set; }
        public string? DeploymentEnvironmentIdentifier { get; set; }
        public string? InstanceOfIdentifier { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Adrs { get; set; }
        public string? Docs { get; set; }
        public List<string>? Tags { get; set; }
        public string? GroupId { get; set; }
        public string? Technology { get; set; }
        public string? Instances { get; set; }
        public string? Metadata { get; set; }
        public StructurizrModelElementType Type { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }
        public StructurizrHealthCheck? HealthCheck { get; set; }
        public IEnumerable<string>? DeploymentGroupsIdentiifiers { get; set; }
        public int? InstanceIndex { get; set; }
        public StructurizrModelElement(string modelId, string identifier)
        {
            ModelId = modelId;
            Identifier = identifier;
        }

        
    }
}
