using AutoMapper;
using DocuEye.Structurizr.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.ModelExploder
{
    /// <summary>
    /// Service for exploding model from structurizr json
    /// </summary>
    public class ModelExploderService : IModelExploderService
    {
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mapper">Automapper service</param>
        public ModelExploderService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        /// <summary>
        /// Explodes model from structurizr json
        /// </summary>
        /// <param name="model">Model object from structurizr json</param>
        /// <returns>The result of exploding model elements and relationships from structurizr json</returns>
        public ExplodeResult ExplodeModel(StructurizrModel? model)
        {
            if (model == null)
            {
                return new ExplodeResult();
            }
            var result = new ExplodeResult();
            if (model.SoftwareSystems != null)
            {
                foreach (var softwareSystem in model.SoftwareSystems)
                {
                    var softwareSystemElement = mapper.Map<ExplodedElement>(softwareSystem);
                    result.Elements.Add(softwareSystemElement);

                    var systemRelationships = this.ExplodeRelationships(softwareSystem.Relationships);
                    result.Relationships.AddRange(systemRelationships);

                    if (softwareSystem.Containers == null)
                    {
                        continue;
                    }

                    foreach (var container in softwareSystem.Containers)
                    {
                        var containerElement = mapper.Map<ExplodedElement>(container);
                        containerElement.StructurizrParentId = softwareSystemElement.StructurizrId;
                        result.Elements.Add(containerElement);

                        var containerRelationships = this.ExplodeRelationships(container.Relationships);
                        result.Relationships.AddRange(containerRelationships);

                        if (container.Components == null)
                        {
                            continue;
                        }
                        foreach (var component in container.Components)
                        {
                            var componentElement = mapper.Map<ExplodedElement>(component);
                            componentElement.StructurizrParentId = containerElement.StructurizrId;
                            result.Elements.Add(componentElement);

                            var componentRelationships = this.ExplodeRelationships(component.Relationships);
                            result.Relationships.AddRange(componentRelationships);
                        }
                    }
                }
            }

            if (model.People != null)
            {
                foreach (var person in model.People)
                {
                    var personElement = mapper.Map<ExplodedElement>(person);
                    result.Elements.Add(personElement);

                    var personRelationships = this.ExplodeRelationships(person.Relationships);
                    result.Relationships.AddRange(personRelationships);
                }
            }

            if (model.DeploymentNodes != null)
            {
                foreach (var deploymentNode in model.DeploymentNodes)
                {
                    var (deploymentNodeElements, deploymentNodeRelations) = ExplodeDeplymentNode(deploymentNode);
                    result.Elements.AddRange(deploymentNodeElements);
                    result.Relationships.AddRange(deploymentNodeRelations);
                }
            }

            return result;
        }

        private (List<ExplodedElement>, List<ExplodedRelationship>) ExplodeDeplymentNode(StructurizrDeploymentNode node, string? parentStructurizrId = null)
        {
            var elements = new List<ExplodedElement>();
            var relationships = new List<ExplodedRelationship>();

            var nodeElement = mapper.Map<ExplodedElement>(node);
            nodeElement.StructurizrParentId = parentStructurizrId;
            elements.Add(nodeElement);

            var nodeRelationships = this.ExplodeRelationships(node.Relationships);
            relationships.AddRange(nodeRelationships);

            if (node.ContainerInstances != null)
            {
                foreach(var instance in node.ContainerInstances)
                {
                    var instanceElement = mapper.Map<ExplodedElement>(instance);
                    instanceElement.StructurizrParentId = node.Id;
                    elements.Add(instanceElement);

                    var instanceElementRelationships = this.ExplodeRelationships(instance.Relationships);
                    relationships.AddRange(instanceElementRelationships);
                }
            }

            if (node.SoftwareSystemInstances != null)
            {
                foreach (var instance in node.SoftwareSystemInstances)
                {
                    var instanceElement = mapper.Map<ExplodedElement>(instance);
                    instanceElement.StructurizrParentId = node.Id;
                    elements.Add(instanceElement);

                    var instanceElementRelationships = this.ExplodeRelationships(instance.Relationships);
                    relationships.AddRange(instanceElementRelationships);
                }
            }

            if (node.InfrastructureNodes != null)
            {
                foreach (var infrastructureNode in node.InfrastructureNodes)
                {
                    var infrastructureElement = mapper.Map<ExplodedElement>(infrastructureNode);
                    infrastructureElement.StructurizrParentId = node.Id;
                    elements.Add(infrastructureElement);

                    var infrastructureElementRelationships = this.ExplodeRelationships(infrastructureNode.Relationships);
                    relationships.AddRange(infrastructureElementRelationships);
                }
            }

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var (children,childRelations) = ExplodeDeplymentNode(child, node.Id);
                    elements.AddRange(children);
                    relationships.AddRange(childRelations);
                }
            }
            return (elements, relationships);
        }

        private List<ExplodedRelationship> ExplodeRelationships(IEnumerable<StructurizrRelationship>? structurizrRelations)
        {
            var relationships = new List<ExplodedRelationship>();
            if(structurizrRelations == null)
            {
                return relationships;
            }
            foreach(var structurizrRelation in structurizrRelations)
            {
                var relationship = this.mapper.Map<ExplodedRelationship>(structurizrRelation);
                relationships.Add(relationship);
            }
            return relationships;
        }
    }
}
