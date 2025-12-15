using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrDeploymentNode
    {
        public string Identifier { get; } = null!;
        public string DeploymentEnvironmentIdentifier { get; set; } = null!;
        public string? ParentNodeIdentifier { get; private set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Instances { get; set; }
        public string? Technology { get; set; }
        public List<string> Tags { get; set; }
        public string? GroupId { get; set; }
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }

        public StructurizrDeploymentNode(string identifier, string environmentIdentifier, string? parentNodeIdentifier)
        {
            Identifier = identifier;
            DeploymentEnvironmentIdentifier = environmentIdentifier;
            ParentNodeIdentifier = parentNodeIdentifier;
            Tags = new List<string>();
        }

        public StructurizrDeploymentNode(string identifier, string environmentIdentifier, string? parentNodeIdentifier, string[] tags)
        {
            Identifier = identifier;
            DeploymentEnvironmentIdentifier = environmentIdentifier;
            ParentNodeIdentifier = parentNodeIdentifier;
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
                Type = StructurizrModelElementType.DeploymentNode,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                GroupId = this.GroupId,
                Technology = this.Technology,
                ParentIdentifier = this.ParentNodeIdentifier,
                DeploymentEnvironmentIdentifier = this.DeploymentEnvironmentIdentifier,
                Instances = this.Instances
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Name = element.Name;
            this.Description = element.Description;
            this.Url = element.Url;
            this.Tags = element.Tags ?? new List<string>();
            this.Properties = element.Properties;
            this.GroupId = element.GroupId;
            this.Technology = element.Technology;
            this.ParentNodeIdentifier = element.ParentIdentifier;
            this.DeploymentEnvironmentIdentifier = element.DeploymentEnvironmentIdentifier!;
            this.Instances = element.Instances;
        }
    }
}
