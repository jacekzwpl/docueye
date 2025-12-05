using System;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrModel
    {
        public string Identifiers { get; set; } = "flat";
        public List<StructurizrRelationship> Relationships { get; set; } = new List<StructurizrRelationship>();
        public List<StructurizrModelElement> Elements { get; set; } = new List<StructurizrModelElement>();
        public List<StructurizrGroup> Groups { get; set; } = new List<StructurizrGroup>();
        public List<StructurizrDeploymentEnvironment> DeploymentEnvironments { get; set; } = new List<StructurizrDeploymentEnvironment>();
        public List<StructurizrDeploymentGroup> DeploymentGroups { get; set; } = new List<StructurizrDeploymentGroup>();
        public IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        
        public void AddRelationship(StructurizrRelationship relationship)
        {
            relationship.ModelId = (this.Relationships.Count() + 1).ToString();
            this.Relationships.Add(relationship);
        }
        
        public void AddModelElement(StructurizrPerson person)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(person.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrSoftwareSystem system)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(system.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrContainer container)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(container.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrComponent component)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(component.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrCustomElement element)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(element.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrDeploymentNode element)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(element.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrInfrastructureNode element)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            this.Elements.Add(element.ToModelElement(modelId));
        }

        public void AddModelElement(StructurizrSoftwareSystemInstance element)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            var systemElement = this.Elements.FirstOrDefault(e => e.Identifier == element.SoftwareSystemIdentifier);
            if (systemElement == null)
            {
                throw new ArgumentException($"Software System with identifier {element.SoftwareSystemIdentifier} not found.");
            }
            
            var count = this.Elements.Count(o => o.InstanceOfIdentifier == element.SoftwareSystemIdentifier);

            var modelElement = element.ToModelElement(modelId);
            modelElement.Name = systemElement.Name;
            modelElement.Technology = systemElement.Technology;
            modelElement.InstanceIndex = count + 1;

            this.Elements.Add(modelElement);
        }

        public void AddModelElement(StructurizrContainerInstance element)
        {
            var modelId = (this.Elements.Count() + 1).ToString();
            var systemElement = this.Elements.FirstOrDefault(e => e.Identifier == element.ContainerIdentifier);
            if (systemElement == null)
            {
                throw new ArgumentException($"Container with identifier {element.ContainerIdentifier} not found.");
            }

            var count = this.Elements.Count(o => o.InstanceOfIdentifier == element.ContainerIdentifier);
            var modelElement = element.ToModelElement(modelId);
            modelElement.Name = systemElement.Name;
            modelElement.Technology = systemElement.Technology;
            modelElement.InstanceIndex = count + 1;
            this.Elements.Add(modelElement);
        }

        public bool ElementIdentifierExists(string identifier)
        {
            return this.Elements.Exists(e => e.Identifier == identifier);
        }

        public StructurizrModelElement? GetModelElement(string identifier)
        {
            return this.Elements.FirstOrDefault(e => e.Identifier == identifier);
        }

        public bool DeploymentGroupIdentifierExists(string identifier)
        {
            return this.DeploymentGroups.Exists(e => e.Identifier == identifier);
        }

        public bool DeploymentEnvironmentIdentifierExists(string identifier)
        {
            return this.DeploymentEnvironments.Exists(e => e.Identifier == identifier);
        }

        public StructurizrDeploymentEnvironment? GetDeploymentEnvironment(string identifier)
        {
            return this.DeploymentEnvironments.FirstOrDefault(e => e.Identifier == identifier);
        }

        public StructurizrRelationship? GetRelationship(string identifier)
        {
            return this.Relationships.FirstOrDefault(r => r.Identifier == identifier);
        }

        public bool AreIdentifiersHierarchical
        {
            get
            {
                return this.Identifiers == "hierarchical";
            }
        }
    }
}
