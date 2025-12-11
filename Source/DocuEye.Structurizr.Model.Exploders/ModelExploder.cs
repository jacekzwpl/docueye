using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;
using DocuEye.Structurizr.Json.Model.Maps;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class ModelExploder
    {

        public ModelExploder()
        {
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeModelElements(StructurizrJsonModel? model)
        {
            if (model == null)
            {
                return (Enumerable.Empty<ElementToImport>(), Enumerable.Empty<RelationshipToImport>());
            }

            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();

            if (model.SoftwareSystems != null)
            {
                var (modelElements,modelElementsRelationships) = this.ExplodeSoftwareSystems(model.SoftwareSystems);
                elements.AddRange(modelElements);
                relationships.AddRange(modelElementsRelationships);
            }

            if (model.People != null)
            {
                var (people, peopleRelationships) = this.ExplodePeople(model.People);
                elements.AddRange(people);
                relationships.AddRange(peopleRelationships);
            }

            if (model.DeploymentNodes != null)
            {
                var (nodes, nodesRelationships) = this.ExplodeDeploymentNodes(model.DeploymentNodes, elements);
                elements.AddRange(nodes);
                relationships.AddRange(nodesRelationships);
            }

            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeSoftwareSystems(IEnumerable<StructurizrJsonSoftwareSystem> softwareSystems)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var softwareSystem in softwareSystems)
            {
                var softwareSystemElement = softwareSystem.ConvertToApModel();
                elements.Add(softwareSystemElement);

                var systemRelationships = this.ExplodeRelationships(softwareSystem.Relationships);
                relationships.AddRange(systemRelationships);

                if (softwareSystem.Containers == null)
                {
                    continue;
                }
                
                var (children, childrenRelationships) = this.ExplodeContainers(softwareSystem.Containers, softwareSystemElement.StructurizrId);
                elements.AddRange(children);
                relationships.AddRange(childrenRelationships);
            }
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeContainers(IEnumerable<StructurizrJsonContainer> containers, string parentId)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var container in containers)
            {
                var containerElement = container.ConvertToApModel();
                containerElement.StructurizrParentId = parentId;
                elements.Add(containerElement);

                var containerRelationships = this.ExplodeRelationships(container.Relationships);
                relationships.AddRange(containerRelationships);

                if (container.Components == null)
                {
                    continue;
                }

                var (components, componentsRelationships) = this.ExplodeComponents(container.Components, containerElement.StructurizrId);
                elements.AddRange(components);
                relationships.AddRange(componentsRelationships);
            }
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeComponents(IEnumerable<StructurizrJsonComponent> components, string parentId)
        {
            var elements = components.Select(c =>
            {
                var element = c.ConvertToApModel();
                element.StructurizrParentId = parentId;
                return element;
            });
            var relationships = components.SelectMany(c => this.ExplodeRelationships(c.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodePeople(IEnumerable<StructurizrJsonPerson> people)
        {
            var elements = people.Select(p => p.ConvertToApModel());
            var relationships = people.SelectMany(p => this.ExplodeRelationships(p.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeDeploymentNodes(IEnumerable<StructurizrJsonDeploymentNode> deploymentNodes, IEnumerable<ElementToImport> sourceElements)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var deploymentNode in deploymentNodes)
            {
                var (nodes, nodesRelationships) = this.ExplodeDeploymentNode(deploymentNode, sourceElements);
                elements.AddRange(nodes);
                relationships.AddRange(nodesRelationships);
            }

            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeDeploymentNode(StructurizrJsonDeploymentNode node, IEnumerable<ElementToImport> sourceElements, string? parentStructurizrId = null)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            var nodeElement = node.ConvertToApModel();
            nodeElement.StructurizrParentId = parentStructurizrId;
            elements.Add(nodeElement);

            var nodeRelationships = this.ExplodeRelationships(node.Relationships);
            relationships.AddRange(nodeRelationships);

            if (node.SoftwareSystemInstances != null)
            {
                var (softwareSystemInstances, softwareSystemInstancesRelationships) = this.ExplodeSoftwareSystemInstances(node.SoftwareSystemInstances, sourceElements, node.Id);
                elements.AddRange(softwareSystemInstances);
                relationships.AddRange(softwareSystemInstancesRelationships);
            }

            if(node.ContainerInstances != null)
            {
                var (containerInstances, containerInstancesRelationships) = this.ExplodeContainerInstances(node.ContainerInstances, sourceElements, node.Id);
                elements.AddRange(containerInstances);
                relationships.AddRange(containerInstancesRelationships);
            }


            if (node.InfrastructureNodes != null)
            {
                var (infrastructureNodes, infrastructureNodesRelationships) = this.ExplodeInfrastructureNodes(node.InfrastructureNodes, node.Id);
                elements.AddRange(infrastructureNodes);
                relationships.AddRange(infrastructureNodesRelationships);
            }

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var (children, childrenRelationships) = this.ExplodeDeploymentNode(child, sourceElements, node.Id);
                    elements.AddRange(children);
                    relationships.AddRange(childrenRelationships);
                }
            }

            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeInfrastructureNodes(IEnumerable<StructurizrJsonInfrastructureNode> infrastructureNodes, string parentId)
        {
            var elements = infrastructureNodes.Select(i => { 
                var element = i.ConvertToApModel();
                element.StructurizrParentId = parentId;
                return element;
            });
            var relationships = infrastructureNodes.SelectMany(i => this.ExplodeRelationships(i.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeSoftwareSystemInstances(IEnumerable<StructurizrJsonSoftwareSystemInstance> instances,IEnumerable<ElementToImport> sourceElements, string parentId)
        {
            var elements = new List<ElementToImport>();
            foreach(var instance in instances)
            {
                var sourceElement = sourceElements.FirstOrDefault(o => o.StructurizrId == instance.SoftwareSystemId && o.Type == ElementType.SoftwareSystem);
                if(sourceElement != null)
                {
                    var element = instance.ConvertToApModel();
                    element.StructurizrParentId = parentId;
                    element.Name = sourceElement.Name;
                    element.Description = sourceElement.Description;
                    element.Technology = sourceElement.Technology;
                    element.Url = sourceElement.Url;
                    
                    elements.Add(element);
                }
                
            }

            var relationships = instances.SelectMany(i => this.ExplodeRelationships(i.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeContainerInstances(IEnumerable<StructurizrJsonContainerInstance> instances, IEnumerable<ElementToImport> sourceElements, string parentId)
        {
            var elements = new List<ElementToImport>();
            foreach (var instance in instances)
            {
                var sourceElement = sourceElements.FirstOrDefault(o => o.StructurizrId == instance.ContainerId && o.Type == ElementType.Container);
                if (sourceElement != null)
                {
                    var element = instance.ConvertToApModel();
                    element.StructurizrParentId = parentId;
                    element.Name = sourceElement.Name;
                    element.Description = sourceElement.Description;
                    element.Technology = sourceElement.Technology;
                    element.Url = sourceElement.Url;

                    elements.Add(element);
                }

            }
            var relationships = instances.SelectMany(i => this.ExplodeRelationships(i.Relationships));
            return (elements, relationships);
        }

        public IEnumerable<RelationshipToImport> ExplodeRelationships(IEnumerable<StructurizrJsonRelationship>? structurizrRelations)
        {
            if (structurizrRelations == null)
            {
                return Enumerable.Empty<RelationshipToImport>();
            }

            return structurizrRelations.ConvertToApiModel();
        }
    }
}
