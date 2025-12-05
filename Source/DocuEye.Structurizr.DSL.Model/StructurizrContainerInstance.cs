using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrContainerInstance
    {
        public string Identifier { get; }
        public string ContainerIdentifier { get; set; }
        public string DeploymentNodeIdentifier { get; set; }
        public string? DeploymentEnvironmentIdentifier { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? GroupId { get; set; }
        public List<string> Tags { get; set; } = null!;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }
        public StructurizrHealthCheck? HealthCheck { get; set; }
        public IEnumerable<string>? DeploymentGroupsIdentiifiers { get; set; }

        public StructurizrContainerInstance(string identifier, string containerIdentifier, string deploymentNodeIdentifier)
        {
            Identifier = identifier;
            Tags = new List<string>();
            ContainerIdentifier = containerIdentifier;
            DeploymentNodeIdentifier = deploymentNodeIdentifier;
        }

        public StructurizrContainerInstance(string identifier, string containerIdentifier, string deploymentNodeIdentifier, string[] tags)
        {
            Identifier = identifier;
            ContainerIdentifier = containerIdentifier;
            DeploymentNodeIdentifier = deploymentNodeIdentifier;
            Tags = new List<string>(tags);
        }

        public StructurizrModelElement ToModelElement(string modelId = "")
        {
            return new StructurizrModelElement(modelId, this.Identifier)
            {
                Description = this.Description,
                Url = this.Url,
                Tags = this.Tags,
                Type = StructurizrModelElementType.ContainerInstance,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                GroupId = this.GroupId,
                ParentIdentifier = this.DeploymentNodeIdentifier,
                InstanceOfIdentifier = this.ContainerIdentifier,
                DeploymentGroupsIdentiifiers = this.DeploymentGroupsIdentiifiers,
                DeploymentEnvironmentIdentifier = this.DeploymentEnvironmentIdentifier,
                HealthCheck = this.HealthCheck
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Description = element.Description;
            this.Url = element.Url;
            this.Tags = element.Tags ?? new List<string>();
            this.Properties = element.Properties;
            this.Perspectives = element.Perspectives;
            this.GroupId = element.GroupId;
            this.DeploymentNodeIdentifier = element.ParentIdentifier!;
            this.ContainerIdentifier = element.InstanceOfIdentifier!;
            this.DeploymentGroupsIdentiifiers = element.DeploymentGroupsIdentiifiers;
            this.DeploymentEnvironmentIdentifier = element.DeploymentEnvironmentIdentifier;
            this.HealthCheck = element.HealthCheck;
        }
    }
}
