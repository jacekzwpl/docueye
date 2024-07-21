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

            var result = new List<ElementToImport>();

            if (model.SoftwareSystems != null)
            {
                var modelElements = this.ExplodeSoftwareSystems(model.SoftwareSystems);
                result.AddRange(modelElements);
            }

            if (model.People != null)
            {
                var people = this.ExplodePeople(model.People);
                result.AddRange(people);
            }

            if (model.DeploymentNodes != null)
            {
                var nodes = this.ExplodeDeploymentNodes(model.DeploymentNodes);
                result.AddRange(nodes);
            }

            return result;
        }

        public IEnumerable<ElementToImport> ExplodeSoftwareSystems(IEnumerable<StructurizrSoftwareSystem> softwareSystems)
        {
            var result = new List<ElementToImport>();
            foreach (var softwareSystem in softwareSystems)
            {
                var softwareSystemElement = this.mapper.Map<ElementToImport>(softwareSystem);
                result.Add(softwareSystemElement);

                if (softwareSystem.Containers == null)
                {
                    continue;
                }

                var containersAndComponents = this.ExplodeContainers(softwareSystem.Containers, softwareSystemElement.StructurizrId);
                result.AddRange(containersAndComponents);
            }
            return result;
        }

        public IEnumerable<ElementToImport> ExplodeContainers(IEnumerable<StructurizrContainer> containers, string parentId)
        {
            var result = new List<ElementToImport>();
            foreach (var container in containers)
            {
                var containerElement = this.mapper.Map<ElementToImport>(container);
                containerElement.StructurizrParentId = parentId;
                result.Add(containerElement);

                if (container.Components == null)
                {
                    continue;
                }

                var components = this.ExplodeComponents(container.Components, containerElement.StructurizrId);
                result.AddRange(components);
            }
            return result;
        }

        public IEnumerable<ElementToImport> ExplodeComponents(IEnumerable<StructurizrComponent> components, string parentId)
        {
            return components.Select(c =>
            {
                var element = this.mapper.Map<ElementToImport>(c);
                element.StructurizrParentId = parentId;
                return element;
            });
        }

        public IEnumerable<ElementToImport> ExplodePeople(IEnumerable<StructurizrPerson> people)
        {
            return people.Select(p => this.mapper.Map<ElementToImport>(p));
        }

        public IEnumerable<ElementToImport> ExplodeDeploymentNodes(IEnumerable<StructurizrDeploymentNode> deploymentNodes)
        {
            var result = new List<ElementToImport>();
            foreach (var deploymentNode in deploymentNodes)
            {
                var nodes = this.ExplodeDeploymentNode(deploymentNode);
                result.AddRange(nodes);
            }

            return result;
        }

        public IEnumerable<ElementToImport> ExplodeDeploymentNode(StructurizrDeploymentNode node, string? parentStructurizrId = null)
        {
            var result = new List<ElementToImport>();
            var nodeElement = mapper.Map<ElementToImport>(node);
            nodeElement.StructurizrParentId = parentStructurizrId;
            result.Add(nodeElement);

            if (node.SoftwareSystemInstances != null)
            {
                var softwareSystemInstances = this.ExplodeSoftwareSystemInstances(node.SoftwareSystemInstances, node.Id);
                result.AddRange(softwareSystemInstances);
            }

            if(node.ContainerInstances != null)
            {
                var containerInstances = this.ExplodeContainerInstances(node.ContainerInstances, node.Id);
                result.AddRange(containerInstances);
            }


            if (node.InfrastructureNodes != null)
            {
                var infrastructureNodes = this.ExplodeInfrastructureNodes(node.InfrastructureNodes, node.Id);
                result.AddRange(infrastructureNodes);
            }

            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    var children = this.ExplodeDeploymentNode(child, node.Id);
                    result.AddRange(children);
                }
            }

            return result;
        }

        public IEnumerable<ElementToImport> ExplodeInfrastructureNodes(IEnumerable<StructurizrInfrastructureNode> infrastructureNodes, string parentId)
        {
            return infrastructureNodes.Select(i => { 
                var element = this.mapper.Map<ElementToImport>(i);
                element.StructurizrParentId = parentId;
                return element;
            });
        }

        public IEnumerable<ElementToImport> ExplodeSoftwareSystemInstances(IEnumerable<StructurizrSoftwareSystemInstance> instances, string parentId)
        {
            return instances.Select(i => { 
                var element = this.mapper.Map<ElementToImport>(i);
                element.StructurizrParentId = parentId;
                return element;
            });
        }

        public IEnumerable<ElementToImport> ExplodeContainerInstances(IEnumerable<StructurizrContainerInstance> instances, string parentId)
        {
            return instances.Select(i => { 
                var element = this.mapper.Map<ElementToImport>(i);
                element.StructurizrParentId = parentId;
                return element;
            });
        }
    }
}
