using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrSoftwareSystemInstance
    {
        public string Identifier { get; }
        public string SoftwareSystemIdentifier { get; set; }
        public string DeploymentNodeIdentifier { get; set; }
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? GroupId { get; set; }
        public List<string> Tags { get; set; } = null!;
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public IEnumerable<StructurizrPerspective>? Perspectives { get; set; }
        public StructurizrHealthCheck? HealthCheck { get; set; }
        public IEnumerable<string>? DeploymentGroupsIdentiifiers { get; set; }

        public StructurizrSoftwareSystemInstance(string identifier, string softwareSystemIdentifier, string deploymentNodeIdentifier)
        {
            Identifier = identifier;
            Tags = new List<string>();
            SoftwareSystemIdentifier = softwareSystemIdentifier;
            DeploymentNodeIdentifier = deploymentNodeIdentifier;
        }

        public StructurizrSoftwareSystemInstance(string identifier, string softwareSystemIdentifier, string deploymentNodeIdentifier, string[] tags)
        {
            Identifier = identifier;
            SoftwareSystemIdentifier = softwareSystemIdentifier;
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
                Type = StructurizrModelElementType.SoftwareSystemInstance,
                Properties = this.Properties,
                Perspectives = this.Perspectives,
                GroupId = this.GroupId,
                ParentIdentifier = this.DeploymentNodeIdentifier,
                InstanceOfIdentifier = this.SoftwareSystemIdentifier,
                DeploymentGroupsIdentiifiers = this.DeploymentGroupsIdentiifiers,
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
            this.SoftwareSystemIdentifier = element.InstanceOfIdentifier!;
            this.DeploymentGroupsIdentiifiers = element.DeploymentGroupsIdentiifiers;
            this.HealthCheck = element.HealthCheck;
        }
    }
}
