using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Application.Events;
using DocuEye.WorkspaceImporter.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors
{
    public class ElementsChangeDetector
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public ElementsChangeDetector(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }


        public async Task<ElementsChangeDetectorResult> DetectSoftwareSystemsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.SoftwareSystem);

        public async Task<ElementsChangeDetectorResult> DetectContainersChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.Container, ElementType.SoftwareSystem);

        public async Task<ElementsChangeDetectorResult> DetectComponentsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.Component, ElementType.Container);

        public async Task<ElementsChangeDetectorResult> DetectPeopleChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.Person);

        public async Task<ElementsChangeDetectorResult> DetectSoftwareSystemInstancesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.SoftwareSystemInstance, ElementType.DeploymentNode);

        public async Task<ElementsChangeDetectorResult> DetectContainerInstancesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.ContainerInstance, ElementType.DeploymentNode);

        public async Task<ElementsChangeDetectorResult> DetectInfrastructureNodesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.InfrastructureNode, ElementType.DeploymentNode);

        public async Task<ElementsChangeDetectorResult> DetectDeploymentNodesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements)
            => await this.DetectChanges(workspaceId, import, elementsToImport, existingElements, ElementType.DeploymentNode, ElementType.DeploymentNode);

        private async Task<ElementsChangeDetectorResult> DetectChanges(string worspaceId, WorkspaceImport import, IEnumerable<ElementToImport> elementsToImport, ICollection<Element> existingElements, string elementType, string? parentElementType = null)
        {
            var result = new ElementsChangeDetectorResult();

            foreach (var elementToImport in elementsToImport.Where(o => o.Type == elementType))
            {
                // Check if element already exists
                var existingElement = existingElements
                    .FirstOrDefault(o => o.DslId == elementToImport.DslId
                        && o.Type == elementType);

                //Apply changes
                if (existingElement == null)
                {
                    var newElement = this.mapper.Map<Element>(elementToImport);
                    newElement.Id = Guid.NewGuid().ToString();
                    newElement.WorkspaceId = worspaceId;
                    newElement.ParentId = this.DiscoverParentId(existingElements, elementToImport, parentElementType);
                    var baseElement = this.DiscoverBaseElementForInstance(elementToImport, existingElements);
                    if (baseElement != null)
                    {
                        newElement.InstanceOfId = baseElement.Id;
                        newElement.Name = baseElement.Name;
                        newElement.Description = baseElement.Description;
                        newElement.Technology = baseElement.Technology;
                    }

                    await this.mediator.Publish(new ElementCreatedEvent(
                        newElement.WorkspaceId,
                        newElement.Id,
                        import.Id,
                        newElement.Name,
                        import.Key,
                        import.SourceLink
                        ));


                    existingElements.Add(newElement);

                    result.ElementsToAdd.Add(newElement);
                }
                else
                {
                    this.mapper.Map<ElementToImport, Element>(elementToImport, existingElement);
                    existingElement.ParentId = this.DiscoverParentId(existingElements, elementToImport, parentElementType);
                    var baseElement = this.DiscoverBaseElementForInstance(elementToImport, existingElements);
                    if (baseElement != null)
                    {
                        existingElement.InstanceOfId = baseElement.Id;
                        existingElement.Name = baseElement.Name;
                        existingElement.Description = baseElement.Description;
                        existingElement.Technology = baseElement.Technology;
                    }

                    //TODO : Check if element has changed

                    result.ElementsToChange.Add(existingElement);
                }
            }
            //Mark elements to delete
            var elementsToDelete = existingElements
                .Where(oldElements =>
                    oldElements.Type == elementType &&
                    !elementsToImport.Any(newElements => newElements.DslId == oldElements.DslId))
                .ToList();
            
            result.ElementsToDelete.AddRange(elementsToDelete);

            //send delete events
            foreach (var elementToDelete in elementsToDelete)
            {
                //Remove from existing elements
                existingElements.Remove(elementToDelete);
                //Send delete event
                await this.mediator.Publish(new ElementDeletedEvent(
                    elementToDelete.WorkspaceId,
                    elementToDelete.Id,
                    import.Id,
                    import.Key,
                    import.SourceLink,
                    elementToDelete.Name
                    ));
            }

            return result;
        }

        private string? DiscoverParentId(IEnumerable<Element> existingElements, ElementToImport element, string? parentElementType) {
            if (!string.IsNullOrEmpty(element.StructurizrParentId) && !string.IsNullOrEmpty(parentElementType))
            {
                var parentNewElement = existingElements
                    .Where(o => o.Type == parentElementType
                        && o.StructurizrId == element.StructurizrParentId)
                    .FirstOrDefault();
                if (parentNewElement != null)
                {
                    return parentNewElement.Id;
                }
            }
            return null;
        }


        private Element? DiscoverBaseElementForInstance(ElementToImport elementToImport, IEnumerable<Element> existingElements)
        {
            if (elementToImport.Type == ElementType.SoftwareSystemInstance)
            {
                return existingElements
                    .FirstOrDefault(o => o.Type == ElementType.SoftwareSystem
                        && o.StructurizrId == elementToImport.StructurizrInstanceOfId);

            }
            else if (elementToImport.Type == ElementType.ContainerInstance)
            {
                return existingElements
                    .FirstOrDefault(o => o.Type == ElementType.Container
                        && o.StructurizrId == elementToImport.StructurizrInstanceOfId);
            }
            return null;
        }
    }
}
