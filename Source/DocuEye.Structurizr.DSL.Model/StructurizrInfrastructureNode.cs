using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrInfrastructureNode
    {
        public string Identifier { get; }
        public string DeploymentNodeIdentifier { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Technology { get; set; }
        public string? Url { get; set; }
        public List<string> Tags { get; set; }
        public string? GroupId { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }

        public StructurizrInfrastructureNode(string identifier, string deploymentNodeIdentifier)
        {
            Identifier = identifier;
            DeploymentNodeIdentifier = deploymentNodeIdentifier;
            Tags = new List<string>();
        }

        public StructurizrInfrastructureNode(string identifier, string deploymentNodeIdentifier, string[] tags)
        {
            Identifier = identifier;
            DeploymentNodeIdentifier = deploymentNodeIdentifier;
            Tags = new List<string>(tags);
        }

        public StructurizrModelElement ToModelElement(string modelId = "")
        {
            return new StructurizrModelElement(modelId, this.Identifier)
            {
                Name = this.Name,
                Description = this.Description,
                Url = this.Url,
                Tags = this.Tags,
                Type = StructurizrModelElementType.InfrastructureNode,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                GroupId = this.GroupId,
                Technology = this.Technology,
                ParentIdentifier = this.DeploymentNodeIdentifier
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Name = element.Name;
            this.Description = element.Description;
            this.Url = element.Url;
            this.Tags = element.Tags ?? new List<string>();
            this.Properties = element.Properties;
            this.Perspectives = element.Perspectives;
            this.GroupId = element.GroupId;
            this.Technology = element.Technology;
            this.DeploymentNodeIdentifier = element.ParentIdentifier ?? throw new InvalidOperationException("ParentIdentifier is required for InfrastructureNode");
        }
    }
}
