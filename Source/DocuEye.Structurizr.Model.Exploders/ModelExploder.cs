using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.Structurizr.Model.Exploders
{
    public class ModelExploder
    {
        private readonly IMapper mapper;

        public ModelExploder(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeModelElements(StructurizrModel? model)
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeSoftwareSystems(IEnumerable<StructurizrSoftwareSystem> softwareSystems)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var softwareSystem in softwareSystems)
            {
                var softwareSystemElement = this.mapper.Map<ElementToImport>(softwareSystem);
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeContainers(IEnumerable<StructurizrContainer> containers, string parentId)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var container in containers)
            {
                var containerElement = this.mapper.Map<ElementToImport>(container);
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeComponents(IEnumerable<StructurizrComponent> components, string parentId)
        {
            var elements = components.Select(c =>
            {
                var element = this.mapper.Map<ElementToImport>(c);
                element.StructurizrParentId = parentId;
                return element;
            });
            var relationships = components.SelectMany(c => this.ExplodeRelationships(c.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodePeople(IEnumerable<StructurizrPerson> people)
        {
            var elements = people.Select(p => this.mapper.Map<ElementToImport>(p));
            var relationships = people.SelectMany(p => this.ExplodeRelationships(p.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeDeploymentNodes(IEnumerable<StructurizrDeploymentNode> deploymentNodes, IEnumerable<ElementToImport> sourceElements)
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeDeploymentNode(StructurizrDeploymentNode node, IEnumerable<ElementToImport> sourceElements, string? parentStructurizrId = null)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            var nodeElement = mapper.Map<ElementToImport>(node);
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeInfrastructureNodes(IEnumerable<StructurizrInfrastructureNode> infrastructureNodes, string parentId)
        {
            var elements = infrastructureNodes.Select(i => { 
                var element = this.mapper.Map<ElementToImport>(i);
                element.StructurizrParentId = parentId;
                return element;
            });
            var relationships = infrastructureNodes.SelectMany(i => this.ExplodeRelationships(i.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeSoftwareSystemInstances(IEnumerable<StructurizrSoftwareSystemInstance> instances,IEnumerable<ElementToImport> sourceElements, string parentId)
        {
            var elements = new List<ElementToImport>();
            foreach(var instance in instances)
            {
                var sourceElement = sourceElements.FirstOrDefault(o => o.StructurizrId == instance.SoftwareSystemId && o.Type == ElementType.SoftwareSystem);
                if(sourceElement != null)
                {
                    var element = this.mapper.Map<ElementToImport>(instance);
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeContainerInstances(IEnumerable<StructurizrContainerInstance> instances, IEnumerable<ElementToImport> sourceElements, string parentId)
        {
            var elements = new List<ElementToImport>();
            foreach (var instance in instances)
            {
                var sourceElement = sourceElements.FirstOrDefault(o => o.StructurizrId == instance.ContainerId && o.Type == ElementType.Container);
                if (sourceElement != null)
                {
                    var element = this.mapper.Map<ElementToImport>(instance);
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

        public IEnumerable<RelationshipToImport> ExplodeRelationships(IEnumerable<StructurizrRelationship>? structurizrRelations)
        {
            if (structurizrRelations == null)
            {
                return Enumerable.Empty<RelationshipToImport>();
            }

            return this.mapper.Map<IEnumerable<RelationshipToImport>>(structurizrRelations);
        }
    }
}
