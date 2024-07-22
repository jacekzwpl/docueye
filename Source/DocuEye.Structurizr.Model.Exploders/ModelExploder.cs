using AutoMapper;
using DocuEye.WorkspaceImporter.Api.Model;
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

        public IEnumerable<ElementToImport> ExplodeModelElements(StructurizrModel? model)
        {
            if (model == null)
            {
                return Enumerable.Empty<ElementToImport>();
            }

            var elements = new List<ElementToImport>();

            if (model.SoftwareSystems != null)
            {
                var (modelElements,modelElementsRelationships) = this.ExplodeSoftwareSystems(model.SoftwareSystems);
                elements.AddRange(modelElements);
            }

            if (model.People != null)
            {
                var (people, peopleRelationships) = this.ExplodePeople(model.People);
                elements.AddRange(people);
            }

            if (model.DeploymentNodes != null)
            {
                var (nodes, nodesRelationships) = this.ExplodeDeploymentNodes(model.DeploymentNodes);
                elements.AddRange(nodes);
            }

            return elements;
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeSoftwareSystems(IEnumerable<StructurizrSoftwareSystem> softwareSystems)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var softwareSystem in softwareSystems)
            {
                var softwareSystemElement = this.mapper.Map<ElementToImport>(softwareSystem);
                elements.Add(softwareSystemElement);

                if (softwareSystem.Containers == null)
                {
                    continue;
                }
                
                var (children, childrenRelationships) = this.ExplodeContainers(softwareSystem.Containers, softwareSystemElement.StructurizrId);
                elements.AddRange(children);
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

                if (container.Components == null)
                {
                    continue;
                }

                var (components, componentsRelationships) = this.ExplodeComponents(container.Components, containerElement.StructurizrId);
                elements.AddRange(components);
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeDeploymentNodes(IEnumerable<StructurizrDeploymentNode> deploymentNodes)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            foreach (var deploymentNode in deploymentNodes)
            {
                var (nodes, nodesRelationships) = this.ExplodeDeploymentNode(deploymentNode);
                elements.AddRange(nodes);
            }

            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeDeploymentNode(StructurizrDeploymentNode node, string? parentStructurizrId = null)
        {
            var elements = new List<ElementToImport>();
            var relationships = new List<RelationshipToImport>();
            var nodeElement = mapper.Map<ElementToImport>(node);
            nodeElement.StructurizrParentId = parentStructurizrId;
            elements.Add(nodeElement);

            if (node.SoftwareSystemInstances != null)
            {
                var (softwareSystemInstances, softwareSystemInstancesRelationships) = this.ExplodeSoftwareSystemInstances(node.SoftwareSystemInstances, node.Id);
                elements.AddRange(softwareSystemInstances);
            }

            if(node.ContainerInstances != null)
            {
                var (containerInstances, containerInstancesRelationships) = this.ExplodeContainerInstances(node.ContainerInstances, node.Id);
                elements.AddRange(containerInstances);
            }


            if (node.InfrastructureNodes != null)
            {
                var (infrastructureNodes, infrastructureNodesRelationships) = this.ExplodeInfrastructureNodes(node.InfrastructureNodes, node.Id);
                elements.AddRange(infrastructureNodes);
            }

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var (children, childrenRelationships) = this.ExplodeDeploymentNode(child, node.Id);
                    elements.AddRange(children);
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

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeSoftwareSystemInstances(IEnumerable<StructurizrSoftwareSystemInstance> instances, string parentId)
        {
            var elements = instances.Select(i => { 
                var element = this.mapper.Map<ElementToImport>(i);
                element.StructurizrParentId = parentId;
                return element;
            });
            var relationships = instances.SelectMany(i => this.ExplodeRelationships(i.Relationships));
            return (elements, relationships);
        }

        public (IEnumerable<ElementToImport>, IEnumerable<RelationshipToImport>) ExplodeContainerInstances(IEnumerable<StructurizrContainerInstance> instances, string parentId)
        {
            var elements = instances.Select(i => { 
                var element = this.mapper.Map<ElementToImport>(i);
                element.StructurizrParentId = parentId;
                return element;
            });
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
