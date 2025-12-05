using DocuEye.Structurizr.DSL.Model;
using DocuEye.Structurizr.Json.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson
{
    public partial class WorkspaceConverter
    {

        public StructurizrJsonModel ConvertModel(StructurizrModel dslModel)
        {
            var jsonModel = new StructurizrJsonModel()
            {
                Properties = dslModel.Properties,
            };
            this.modelGroupSeparator = jsonModel.GroupSeparator;

            if (dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.Person).Any())
            {
                var people = new List<StructurizrJsonPerson>();
                foreach (var person in dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.Person))
                {
                    var jsonPerson = this.ConvertPerson(person);
                    people.Add(jsonPerson);
                }
                jsonModel.People = people.ToArray();
            }

            if (dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.SoftwareSystem).Any())
            {
                var softwareSystems = new List<StructurizrJsonSoftwareSystem>();
                foreach (var softwareSystem in dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.SoftwareSystem))
                {
                    var jsonSoftwareSystem = this.ConvertSoftwareSystem(softwareSystem);
                    softwareSystems.Add(jsonSoftwareSystem);
                }
                jsonModel.SoftwareSystems = softwareSystems.ToArray();
            }

            if (dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.CustomElement).Any())
            {
                var customElements = new List<StructurizrJsonCustomElement>();
                foreach (var customElement in dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.CustomElement))
                {
                    var jsonCustomElement = this.ConvertCustomElement(customElement);
                    customElements.Add(jsonCustomElement);
                }
                jsonModel.CustomElements = customElements.ToArray();
            }

            if (dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.DeploymentNode).Any())
            {
                var deploymentNodes = new List<StructurizrJsonDeploymentNode>();
                foreach (var deploymentNode in dslModel.Elements.Where(o => o.Type == StructurizrModelElementType.DeploymentNode))
                {
                    var jsonDeploymentNode = this.ConvertDeploymentNode(deploymentNode);
                    deploymentNodes.Add(jsonDeploymentNode);
                }
                jsonModel.DeploymentNodes = deploymentNodes.ToArray();
            }


            return jsonModel;
        }

        public StructurizrJsonDeploymentNode ConvertDeploymentNode(StructurizrModelElement element)
        {

            var groups = this.ResolveElementGroups(element.GroupId);
            var environmentName = this.dslWorkspace.Model.DeploymentEnvironments.Where(o => o.Identifier == element.DeploymentEnvironmentIdentifier).FirstOrDefault()?.Name;
            var jsonDeploymentNode = new StructurizrJsonDeploymentNode()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Technology = element.Technology,
                Environment = environmentName,
                Instances = element.Instances,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier),
                ContainerInstances = new List<StructurizrJsonContainerInstance>(),
                SoftwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>(),
                InfrastructureNodes = new List<StructurizrJsonInfrastructureNode>()
            };
            jsonDeploymentNode.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);

            var childNodeElements = this.dslWorkspace.Model.Elements.Where(o => o.ParentIdentifier == element.Identifier && o.Type == StructurizrModelElementType.DeploymentNode);
            if (childNodeElements.Any())
            {
                jsonDeploymentNode.Children = new List<StructurizrJsonDeploymentNode>();
                foreach (var childNodeElement in childNodeElements)
                {
                    var jsonChildNode = this.ConvertDeploymentNode(childNodeElement);
                    (jsonDeploymentNode.Children as List<StructurizrJsonDeploymentNode>)?.Add(jsonChildNode);
                }
            }

            var containerInstances = this.dslWorkspace.Model.Elements.Where(o => o.ParentIdentifier == element.Identifier && o.Type == StructurizrModelElementType.ContainerInstance);
            if (containerInstances.Any())
            {
                jsonDeploymentNode.ContainerInstances = new List<StructurizrJsonContainerInstance>();
                foreach (var containerInstance in containerInstances)
                {
                    var jsonContainerInstance = this.ConvertContainerInstance(containerInstance);
                    (jsonDeploymentNode.ContainerInstances as List<StructurizrJsonContainerInstance>)?.Add(jsonContainerInstance);
                }
            }

            var softwareSystemInstances = this.dslWorkspace.Model.Elements.Where(o => o.ParentIdentifier == element.Identifier && o.Type == StructurizrModelElementType.SoftwareSystemInstance);
            if (softwareSystemInstances.Any())
            {
                jsonDeploymentNode.SoftwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>();
                foreach (var softwareSystemInstance in softwareSystemInstances)
                {
                    var jsonSoftwareSystemInstance = this.ConvertSoftwareSystemInstance(softwareSystemInstance);
                    (jsonDeploymentNode.SoftwareSystemInstances as List<StructurizrJsonSoftwareSystemInstance>)?.Add(jsonSoftwareSystemInstance);
                }
            }

            var infrastructureNodes = this.dslWorkspace.Model.Elements.Where(o => o.ParentIdentifier == element.Identifier && o.Type == StructurizrModelElementType.InfrastructureNode);
            if (infrastructureNodes.Any())
            {
                jsonDeploymentNode.InfrastructureNodes = new List<StructurizrJsonInfrastructureNode>();
                foreach (var infrastructureNode in infrastructureNodes)
                {
                    var jsonInfrastructureNode = this.ConvertInfrastructureNode(infrastructureNode);
                    (jsonDeploymentNode.InfrastructureNodes as List<StructurizrJsonInfrastructureNode>)?.Add(jsonInfrastructureNode);
                }
            }

            return jsonDeploymentNode;
        }

        public StructurizrJsonContainerInstance ConvertContainerInstance(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            var instanceOfElement = this.dslWorkspace.Model.Elements.Where(o => o.Identifier == element.InstanceOfIdentifier).FirstOrDefault();
            var environmentName = this.dslWorkspace.Model.DeploymentEnvironments.Where(o => o.Identifier == element.DeploymentEnvironmentIdentifier).FirstOrDefault()?.Name;
            var jsonContainerInstance = new StructurizrJsonContainerInstance()
            {
                Id = element.ModelId,
                ContainerId = instanceOfElement?.ModelId ?? "",
                Environment = environmentName,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Properties = element.Properties,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier),
                InstanceId = element.InstanceIndex ?? 1
            };
            jsonContainerInstance.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            return jsonContainerInstance;
        }

        public StructurizrJsonSoftwareSystemInstance ConvertSoftwareSystemInstance(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            var instanceOfElement = this.dslWorkspace.Model.Elements.Where(o => o.Identifier == element.InstanceOfIdentifier).FirstOrDefault();
            var environmentName = this.dslWorkspace.Model.DeploymentEnvironments.Where(o => o.Identifier == element.DeploymentEnvironmentIdentifier).FirstOrDefault()?.Name;
            var jsonSoftwareSystemInstance = new StructurizrJsonSoftwareSystemInstance()
            {
                Id = element.ModelId,
                SoftwareSystemId = instanceOfElement?.ModelId ?? "",
                Environment = environmentName,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Properties = element.Properties,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier),
                InstanceId = element.InstanceIndex ?? 1
            };
            jsonSoftwareSystemInstance.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            return jsonSoftwareSystemInstance;
        }

        public StructurizrJsonInfrastructureNode ConvertInfrastructureNode(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            
            var environmentName = this.dslWorkspace.Model.DeploymentEnvironments.Where(o => o.Identifier == element.DeploymentEnvironmentIdentifier).FirstOrDefault()?.Name;
            var jsonInfrastructureNode = new StructurizrJsonInfrastructureNode()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Technology = element.Technology,
                Environment = environmentName,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier)
            };
            jsonInfrastructureNode.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            return jsonInfrastructureNode;
        }

        public StructurizrJsonCustomElement ConvertCustomElement(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            var jsonCustomElement = new StructurizrJsonCustomElement()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Metadata = element.Metadata,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier)
            };
            jsonCustomElement.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            return jsonCustomElement;
        }

        public StructurizrJsonSoftwareSystem ConvertSoftwareSystem(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            var jsonSoftwareSystem = new StructurizrJsonSoftwareSystem()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Location = "Unspecified",
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier)
            };
            jsonSoftwareSystem.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            List<StructurizrJsonContainer> jsonContainers = new List<StructurizrJsonContainer>();
            foreach (var dslContainer in this.dslWorkspace.Model.Elements.Where(o => o.ParentIdentifier == element.Identifier && o.Type == StructurizrModelElementType.Container))
            {
                var jsonContainer = this.ConvertContainer(dslContainer);
                jsonContainers.Add(jsonContainer);
            }
            jsonSoftwareSystem.Containers = jsonContainers.ToArray();
            jsonSoftwareSystem.Documentation = this.documentationReader.Read(element.Docs, element.Adrs);
            return jsonSoftwareSystem;
        }

        public StructurizrJsonContainer ConvertContainer(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            var jsonContainer = new StructurizrJsonContainer()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier),
                Technology = element.Technology
            };
            jsonContainer.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            var jsonComponents = new List<StructurizrJsonComponent>();
            foreach (var dslComponent in this.dslWorkspace.Model.Elements.Where(o => o.ParentIdentifier == element.Identifier && o.Type == StructurizrModelElementType.Component))
            {
                var jsonComponent = this.ConvertComponent(dslComponent);
                jsonComponents.Add(jsonComponent);
            }
            jsonContainer.Components = jsonComponents.ToArray();
            jsonContainer.Documentation = this.documentationReader.Read(element.Docs, element.Adrs);
            return jsonContainer;
        }

        public StructurizrJsonComponent ConvertComponent(StructurizrModelElement element)
        {
            var groups = this.ResolveElementGroups(element.GroupId);
            var jsonComponent = new StructurizrJsonComponent()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier),
                Technology = element.Technology

            };
            jsonComponent.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            jsonComponent.Documentation = this.documentationReader.Read(element.Docs, element.Adrs);
            return jsonComponent;
        }

        public StructurizrJsonPerson ConvertPerson(StructurizrModelElement element)
        {

            var groups = this.ResolveElementGroups(element.GroupId);
            var jsonPerson = new StructurizrJsonPerson()
            {
                Id = element.ModelId,
                Name = element.Name,
                Description = element.Description,
                Url = element.Url,
                Tags = string.Join(",", element.Tags ?? new List<string>()),
                Group = groups == null ? null : string.Join(this.modelGroupSeparator, groups),
                Properties = element.Properties,
                Location = "Unspecified",
                Relationships = this.ResolveElementRelationships(element.ModelId, element.Identifier)
            };
            jsonPerson.Properties.Add(DslPropertyNames.DslIdProperty, element.Identifier);
            return jsonPerson;
        }

        public IEnumerable<string>? ResolveElementGroups(string? groupId)
        {
            if (groupId == null)
            {
                return null;
            }
            List<string> groups = new List<string>();
            var group = this.dslWorkspace.Model.Groups.Where(o => o.Id == groupId).FirstOrDefault();
            if (group == null)
            {
                return null;
            }
            groups.Add(group.Name);

            if (group.ParentId != null)
            {
                var parentGroups = this.ResolveElementGroups(group.ParentId);
                if (parentGroups != null)
                {
                    groups.AddRange(parentGroups);
                }
            }
            return groups.ToArray();
        }

        public IEnumerable<StructurizrJsonRelationship> ResolveElementRelationships(string elementModelId, string elementIdentifier)
        {
            var relationships = new List<StructurizrJsonRelationship>();
            foreach (var dslRelationship in this.dslWorkspace.Model.Relationships.Where(o => o.SourceIdentifier == elementIdentifier))
            {
                var destinationElement = this.dslWorkspace.Model.Elements.Where(o => o.Identifier == dslRelationship.DestinationIdentifier).FirstOrDefault();
                if (destinationElement == null)
                {
                    continue;
                }
                var jsonRelationship = new StructurizrJsonRelationship()
                {
                    Id = dslRelationship.ModelId,
                    Description = dslRelationship.Description,
                    Url = dslRelationship.Url,
                    Tags = string.Join(",", dslRelationship.Tags ?? new List<string>()),
                    DestinationId = destinationElement.ModelId,
                    Properties = new Dictionary<string, string>(dslRelationship.Properties),
                    SourceId = elementModelId,
                    Technology = dslRelationship.Technology,
                    LinkedRelationshipId = dslRelationship.LinkedRelationshipModelId
                };
                jsonRelationship.Properties.Add(DslPropertyNames.DslIdProperty, dslRelationship.Identifier);
                
                relationships.Add(jsonRelationship);
            }

            return relationships;
        }
    }
}
