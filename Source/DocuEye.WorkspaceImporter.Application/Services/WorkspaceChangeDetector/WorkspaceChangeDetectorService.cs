using AutoMapper;
using DocuEye.DocsKeeper.Model;
using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Events;
using DocuEye.WorkspaceImporter.Application.Services.DecisionsExploder;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.ViewsExploder;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector
{
    /// <summary>
    /// Service for detecting changes in workspace
    /// </summary>
    public class WorkspaceChangeDetectorService : IWorkspaceChangeDetectorService
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mapper">Automapper service</param>
        /// <param name="mediator">Mediator service</param>
        public WorkspaceChangeDetectorService(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        /// <summary>
        /// Detects changes in images
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="documentationId">The ID of documentation that images belong to</param>
        /// <param name="structurizrImages">List of images form structurizr json</param>
        /// <returns>List of images to save in the workspace</returns>
        public IEnumerable<Image> DetectImagesChanges(string workspaceId, string documentationId, IEnumerable<StructurizrImage>? structurizrImages)
        {
            if(structurizrImages == null)
            {
                return Enumerable.Empty<Image>();
            }
            var images = this.mapper.Map<IEnumerable<Image>>(structurizrImages);
            foreach (var image in images)
            {
               image.Id = Guid.NewGuid().ToString();
               image.DocumentationId = documentationId;
               image.WorkspaceId = workspaceId;
            }
            return images;
        }
        /// <summary>
        /// Detects changes in documentation
        /// </summary>
        /// <param name="workspaceId">The id of workspace</param>
        /// <param name="structurizrDocumentation">Documentation object from structurizr json</param>
        /// <returns>Dcoumentation to save in workspace</returns>
        public Documentation DetectDocumentationChanges(string workspaceId, StructurizrDocumentation? structurizrDocumentation)
        {
            var documentation = new Documentation()
            {
                Id = Guid.NewGuid().ToString(),
                WorkspaceId = workspaceId
            };

            if(structurizrDocumentation == null)
            {
                return documentation;
            }

            if (structurizrDocumentation.Sections != null)
            {
                documentation.Sections = this.mapper.Map<IEnumerable<DocumentationSection>>(structurizrDocumentation.Sections);
            }

            return documentation;
        }

        /// <summary>
        /// Detects decisions changes 
        /// </summary>
        /// <param name="worspaceId">The ID of workspace</param>
        /// <param name="documentationId">The ID of documentation that decisions belong to</param>
        /// <param name="elementId">The ID of element that decisions belong to</param>
        /// <param name="newDecisions">Decisions exploded from structurizr json</param>
        /// <returns>List of decisions to save in the workspace</returns>
        public IEnumerable<Decision> DetectDecisionChnages(string worspaceId, string documentationId, string? elementId, IEnumerable<ExplodedDecision> newDecisions)
        {
            List<Decision> decisions = new List<Decision>();
            foreach (var newDecision in newDecisions)
            {
                var links = new List<DecisionLink>();
                if (newDecision.StructurizrLinks != null && newDecision.StructurizrLinks.Any())
                {
                    foreach (var newLink in newDecision.StructurizrLinks)
                    {
                        var linkedDecision = newDecisions.FirstOrDefault(o => o.DslId == newLink.StructurizrId);
                        if(linkedDecision != null)
                        {
                            var link = this.mapper.Map<DecisionLink>(newLink);
                            link.Id = linkedDecision.Id;
                            link.Title = linkedDecision.Title;
                            links.Add(link);
                        }
                    }
                }

                var decision = mapper.Map<Decision>(newDecision);
                decision.Links = links;
                decision.WorkspaceId = worspaceId;
                decision.ElementId = elementId;
                decision.DocumentationId = documentationId;
                decisions.Add(decision);
            }


            return decisions;
        }
        /// <summary>
        /// Detects views changes
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="explodeViewsResult">Result of exploding views from structurizr json</param>
        /// <param name="newElements">Elements exploded form structurizr json</param>
        /// <param name="newRelationships">Relationships exploded from structurizr json</param>
        /// <returns>Result of detecting changes in views</returns>
        public ViewsChangesResult DetectViewsChanges(string workspaceId, ViewsExplodeResult explodeViewsResult, IEnumerable<ExplodedElement> newElements, IEnumerable<ExplodedRelationship> newRelationships)
        {
            var result = new ViewsChangesResult(this.mapper);

            var componentsElementDiagrams = new Dictionary<string, string>();
            var containerElementDiagrams = new Dictionary<string, string>();
            var systemContextElementDiagrams = new Dictionary<string, string>();

            foreach (var explodedView in explodeViewsResult.ComponentViews)
            {
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                var contextElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedView.StructurizrContainerId);
                if (contextElement != null)
                {
                    explodedView.View.ContainerId = contextElement.Id;
                    componentsElementDiagrams.Add(contextElement.Id, explodedView.View.Id);
                }

                var viewElements = this.DetectElementsInView(explodedView.Elements, newElements, new Dictionary<string, string>());
                var viewRelationships = this.DetectRelationshipsView(explodedView.Relationships, newRelationships);
                explodedView.View.Elements = viewElements;
                explodedView.View.Relationships = viewRelationships;

                result.ComponentViewsToAdd.Add(explodedView.View);
            }

            foreach (var explodedView in explodeViewsResult.ContainerViews)
            {
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                var contextElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedView.StructurizrSoftwareSystemId);
                if (contextElement != null)
                {
                    explodedView.View.SoftwareSystemId = contextElement.Id;
                    containerElementDiagrams.Add(contextElement.Id, explodedView.View.Id);
                }

                var viewElements = this.DetectElementsInView(explodedView.Elements, newElements, componentsElementDiagrams);
                var viewRelationships = this.DetectRelationshipsView(explodedView.Relationships, newRelationships);
                explodedView.View.Elements = viewElements;
                explodedView.View.Relationships = viewRelationships;

                result.ContainerViewsToAdd.Add(explodedView.View);
            }

            foreach (var explodedView in explodeViewsResult.SystemContextViews)
            {
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                var contextElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedView.StructurizrSoftwareSystemId);
                if (contextElement != null)
                {
                    explodedView.View.SoftwareSystemId = contextElement.Id;
                    systemContextElementDiagrams.Add(contextElement.Id, explodedView.View.Id);
                }
                var viewElements = this.DetectElementsInView(explodedView.Elements, newElements, containerElementDiagrams);
                var viewRelationships = this.DetectRelationshipsView(explodedView.Relationships, newRelationships);

                explodedView.View.Elements = viewElements;
                explodedView.View.Relationships = viewRelationships;
                result.SystemContextViewsToAdd.Add(explodedView.View);
            }


            foreach (var explodedView in explodeViewsResult.LandscapeViews)
            {
                var viewElements = this.DetectElementsInView(explodedView.Elements, newElements, systemContextElementDiagrams);
                var viewRelationships = this.DetectRelationshipsView(explodedView.Relationships, newRelationships);
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                explodedView.View.Elements = viewElements;
                explodedView.View.Relationships = viewRelationships;
                result.SystemLandscapeViewsToAdd.Add(explodedView.View);
            }

            foreach (var explodedView in explodeViewsResult.DeploymentViews)
            {
                var viewElements = this.DetectElementsInView(explodedView.Elements, newElements, new Dictionary<string, string>());
                var viewRelationships = this.DetectRelationshipsView(explodedView.Relationships, newRelationships);
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                explodedView.View.Elements = viewElements;
                explodedView.View.Relationships = viewRelationships;
                var contextElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedView.StructuruzrSoftwareSystemId);
                if (contextElement != null)
                {
                    explodedView.View.SoftwareSystemId = contextElement.Id;
                }
                result.DeploymentViewsToAdd.Add(explodedView.View);
            }

            foreach (var explodedView in explodeViewsResult.DynamicViews)
            {
                var viewElements = this.DetectElementsInView(explodedView.Elements, newElements, new Dictionary<string, string>());
                var viewRelationships = this.DetectDynamicRelationshipsView(explodedView.Relationships, newRelationships);
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                explodedView.View.Elements = viewElements;
                explodedView.View.Relationships = viewRelationships;
                var contextElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedView.StructurizrElementId);
                if (contextElement != null)
                {
                    explodedView.View.ElementId = contextElement.Id;
                }
                result.DynamicViewsToAdd.Add(explodedView.View);
            }

            foreach (var explodedView in explodeViewsResult.ImagesViews)
            { 
                explodedView.View.Id = Guid.NewGuid().ToString();
                explodedView.View.WorkspaceId = workspaceId;
                var contextElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedView.StructurizrElementId);
                if (contextElement != null)
                {
                    explodedView.View.ElementId = contextElement.Id;
                }
                result.ImagesViewsToAdd.Add(explodedView.View);
            }

            foreach(var filteredView in explodeViewsResult.FilteredViews)
            {
                filteredView.Id = Guid.NewGuid().ToString();
                filteredView.WorkspaceId = workspaceId;
                if(filteredView.Mode?.ToLower() == "exclude")
                {
                    if(!filteredView.Tags.Contains("*"))
                    {
                        filteredView.Elements = result.GetElementsByViewKey(filteredView.BaseViewKey)
                            .Where(o => !o.Tags.Any(tag => filteredView.Tags.Contains(tag))).ToArray();
                        filteredView.Relationships = result.GetRelationshipsByViewKey(filteredView.BaseViewKey)
                            .Where(o => !o.Tags.Any(tag => filteredView.Tags.Contains(tag))).ToArray();
                    }
                }else
                {
                    if (!filteredView.Tags.Contains("*"))
                    {
                        filteredView.Elements = result.GetElementsByViewKey(filteredView.BaseViewKey)
                            .Where(o => o.Tags.Any(tag => filteredView.Tags.Contains(tag))).ToArray();
                        filteredView.Relationships = result.GetRelationshipsByViewKey(filteredView.BaseViewKey)
                            .Where(o => o.Tags.Any(tag => filteredView.Tags.Contains(tag))).ToArray();
                    }else
                    {
                        filteredView.Elements = result.GetElementsByViewKey(filteredView.BaseViewKey).ToArray();
                        filteredView.Relationships = result.GetRelationshipsByViewKey(filteredView.BaseViewKey).ToArray();
                    }
                }

                result.FilteredViewsToAdd.Add(filteredView);
            }


            return result;
        }

        private IEnumerable<DynamicRelationshipView> DetectDynamicRelationshipsView(IEnumerable<ExplodedRelationshipView> explodedRelationships, IEnumerable<ExplodedRelationship> newRelationships)
        {
            var viewRelationships = new List<DynamicRelationshipView>();
            foreach (var explodedRelationship in explodedRelationships)
            {
                var newRelationship = newRelationships
                    .FirstOrDefault(o => o.StructurizrId == explodedRelationship.StructurizrId);
                if (newRelationship != null)
                {
                    var relationshipView = this.mapper.Map<DynamicRelationshipView>(explodedRelationship);
                    relationshipView.Id = Guid.NewGuid().ToString();
                    relationshipView.SourceId = relationshipView.Response.HasValue && relationshipView.Response.Value ? newRelationship.DestinationId : newRelationship.SourceId;
                    relationshipView.DestinationId = relationshipView.Response.HasValue && relationshipView.Response.Value ? newRelationship.SourceId : newRelationship.DestinationId;
                    relationshipView.BaseRelationshipId = newRelationship.Id;
                    viewRelationships.Add(relationshipView);
                }
            }

            return viewRelationships;
        }

        private IEnumerable<RelationshipView> DetectRelationshipsView(IEnumerable<ExplodedRelationshipView> explodedRelationships, IEnumerable<ExplodedRelationship> newRelationships)
        {
            var viewRelationships = new List<RelationshipView>();
            foreach(var explodedRelationship in explodedRelationships)
            {
                var newRelationship = newRelationships
                    .FirstOrDefault(o => o.StructurizrId == explodedRelationship.StructurizrId);
                if (newRelationship != null)
                {
                    explodedRelationship.Id = newRelationship.Id;
                    var relationshipView = this.mapper.Map<RelationshipView>(explodedRelationship);
                    relationshipView = this.mapper.Map<ExplodedRelationship, RelationshipView>(newRelationship, relationshipView);
                    viewRelationships.Add(relationshipView);
                }
            }

            return viewRelationships;
        }

        private IEnumerable<ElementView> DetectElementsInView(IEnumerable<ExplodedElementView> explodedViewElements, IEnumerable<ExplodedElement> newElements, Dictionary<string,string> elementDiagrams)
        {
            var viewElements = new List<ElementView>();
            foreach (var explodedViewElement in explodedViewElements)
            {
                var newElement = newElements
                    .FirstOrDefault(o => o.StructurizrId == explodedViewElement.StructurizrId);
                if (newElement != null)
                {
                    explodedViewElement.Id = newElement.Id;
                    var elementView = this.mapper.Map<ElementView>(explodedViewElement);
                    elementView = this.mapper.Map<ExplodedElement, ElementView>(newElement,elementView);
                    if(elementDiagrams.ContainsKey(newElement.Id))
                    {
                        elementView.DiagramId = elementDiagrams[newElement.Id];
                    }
                    //elementView.DiagramId = newElement.Id == contextElementId ? diagramId : null;
                    viewElements.Add(elementView);
                }
            }
            return viewElements;
        }
        /// <summary>
        /// Detects relationships changes
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="import">Import ID</param>
        /// <param name="newRelationships">Relationships exploded from structurizr json</param>
        /// <param name="existingRelationships">Relationships that exists in workspace</param>
        /// <param name="newElements">Elements exploded from structurizr json</param>
        /// <returns>Result of detecting relationships changes </returns>
        public async Task<RelaionshipsChangesResult> DetectRelaionshipsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedRelationship> newRelationships, IEnumerable<Relationship> existingRelationships, IEnumerable<ExplodedElement> newElements)
        {
            var result = new RelaionshipsChangesResult();

            //First process relationships without link
            foreach (var newRelationship in newRelationships.Where(o => string.IsNullOrEmpty(o.StructurizrLinkedRelationshipId)))
            {
                var sourceElement = newElements
                        .FirstOrDefault(o => o.StructurizrId == newRelationship.StructurizrSourceId);
                var destinationElement = newElements
                        .FirstOrDefault(o => o.StructurizrId == newRelationship.StructurizrDestinationId);

                if (sourceElement == null || destinationElement == null)
                {
                    continue;
                }

                newRelationship.SourceId = sourceElement.Id;
                newRelationship.DestinationId = destinationElement.Id;

                var existingRelationship = existingRelationships
                    .FirstOrDefault(o => o.DslId == newRelationship.DslId);

                if (existingRelationship != null)
                {
                    newRelationship.Id = existingRelationship.Id;
                    newRelationship.WorkspaceId = existingRelationship.WorkspaceId;
                    if (!existingRelationship.IsDataEqual(newRelationship))
                    {
                        result.RelationshipsToChange.Add(mapper.Map<Relationship>(newRelationship));
                        await this.mediator.Publish(new RelationshipChangedEvent(
                            workspaceId,
                            newRelationship.Id,
                            import.Id,
                            import.Key,
                            import.SourceLink,
                            sourceElement.Name,
                            destinationElement.Name));
                        
                    }
                }
                else
                {
                    newRelationship.Id = Guid.NewGuid().ToString();
                    newRelationship.WorkspaceId = workspaceId;
                    result.RelationshipsToAdd.Add(mapper.Map<Relationship>(newRelationship));
                    await this.mediator.Publish(new RelationshipCreatedEvent(
                        workspaceId,
                        newRelationship.Id,
                        import.Id,
                        import.Key,
                        import.SourceLink,
                        sourceElement.Name,
                        destinationElement.Name
                        ));

                }
            }
            // Process relationships with link
            foreach (var newRelationship in newRelationships.Where(o => !string.IsNullOrEmpty(o.StructurizrLinkedRelationshipId)))
            {
                var sourceElement = newElements
                        .FirstOrDefault(o => o.StructurizrId == newRelationship.StructurizrSourceId);
                var destinationElement = newElements
                        .FirstOrDefault(o => o.StructurizrId == newRelationship.StructurizrDestinationId);

                if (sourceElement == null || destinationElement == null)
                {
                    continue;
                }

                newRelationship.SourceId = sourceElement.Id;
                newRelationship.DestinationId = destinationElement.Id;

                var linkedRelationShip = newRelationships
                        .FirstOrDefault(o => o.StructurizrId == newRelationship.StructurizrLinkedRelationshipId);
                if(linkedRelationShip != null)
                {
                    newRelationship.Id = Guid.NewGuid().ToString();
                    newRelationship.WorkspaceId = workspaceId;
                    newRelationship.LinkedRelationshipId = linkedRelationShip.Id;
                    newRelationship.DslId = Guid.NewGuid().ToString();
                    result.RelationshipsToAdd.Add(mapper.Map<Relationship>(newRelationship));
                }
            }

            var relationshipsToDelete = existingRelationships
                .Where(oldRelationships =>
                    !newRelationships.Any(newRelationships => newRelationships.DslId == oldRelationships.DslId))
                .ToList();
            result.RelationshipsToDelete.AddRange(relationshipsToDelete);

            foreach (var relationship in relationshipsToDelete.Where(o => o.LinkedRelationshipId == null)) {
                var sourceElement = newElements
                        .FirstOrDefault(o => o.Id == relationship.SourceId);
                var destinationElement = newElements
                        .FirstOrDefault(o => o.Id == relationship.DestinationId);
                //Track changes only if elements still exists.
                if(sourceElement != null && destinationElement != null)
                {
                    await this.mediator.Publish(new RelationshipDeletedEvent(
                        workspaceId,
                        relationship.Id,
                        import.Id,
                        import.Key,
                        import.SourceLink,
                        sourceElement.Name,
                        destinationElement.Name));
                }
            }

            

            return result;
        }
        /// <summary>
        /// Detects elements changes
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="import">Import Id</param>
        /// <param name="newElements">Elements exploded from structurizr json</param>
        /// <param name="existingElements">Elements that exists in workspace</param>
        /// <returns>Result of detecting elements changes </returns>
        public async Task<ElementsChangesResult> DetectElementsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements) {
            var changesResult = new ElementsChangesResult();

            var systemsResult = await this.DetectSoftwareSystemChanges(workspaceId, import, newElements, existingElements);
            var containersResult = await this.DetectContainersChanges(workspaceId, import, newElements, existingElements);
            var componentsResult = await this.DetectComponentsChanges(workspaceId, import, newElements, existingElements);
            var peopleResult = await this.DetectPeopleChanges(workspaceId, import, newElements, existingElements);
            var deploymentNodesResult = await this.DetectDeploymentNodesChanges(workspaceId, import, newElements, existingElements);
            var containerInstancesResult = await this.DetectContainerInstancesChanges(workspaceId, import, newElements, existingElements);
            var systemInstancesResult = await this.DetectSoftwareSystemInstancesChanges(workspaceId, import, newElements, existingElements);
            var infrastrucureNodesResult = await this.DetectInfrastructureNodesChanges(workspaceId, import, newElements, existingElements);
            changesResult.AddResult(systemsResult);
            changesResult.AddResult(containersResult);
            changesResult.AddResult(componentsResult);
            changesResult.AddResult(peopleResult);
            changesResult.AddResult(deploymentNodesResult);
            changesResult.AddResult(containerInstancesResult);
            changesResult.AddResult(systemInstancesResult);
            changesResult.AddResult(infrastrucureNodesResult);

            return changesResult;
        }

        private async Task<ElementsChangesResult> DetectChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements, string elementType, string? parentElementType = null)
        {
            var result = new ElementsChangesResult();
            foreach (var newElement in newElements.Where(o => o.Type == elementType))
            {
                var existingElement = existingElements
                    .FirstOrDefault(o => o.DslId == newElement.DslId
                        && o.Type == elementType);


                var baseElement = this.GetBaseElementForInstance(newElement, newElements);
                if(baseElement != null)
                {
                    newElement.InstanceOfId = baseElement.Id;
                    newElement.Name = baseElement.Name;
                    newElement.Description = baseElement.Description;
                    newElement.Technology = baseElement.Technology;
                } 


                if (!string.IsNullOrEmpty(newElement.StructurizrParentId) && !string.IsNullOrEmpty(parentElementType))
                {
                    var parentNewElement = newElements
                        .Where(o => o.Type == parentElementType
                            && o.StructurizrId == newElement.StructurizrParentId)
                        .FirstOrDefault();
                    if (parentNewElement != null)
                    {
                        newElement.ParentId = parentNewElement.Id;
                    }
                    
                }
                var elementId = Guid.NewGuid().ToString();
                if (existingElement != null)
                {
                    elementId = existingElement.Id;
                    newElement.Id = existingElement.Id;
                    newElement.WorkspaceId = existingElement.WorkspaceId;
                    if(!existingElement.IsDataEqual(newElement) || newElement.ParentId != existingElement.ParentId)
                    {
                        await this.mediator.Publish(new ElementChangedEvent(
                            newElement.WorkspaceId, 
                            elementId,
                            import.Id,
                            import.Key,
                            import.SourceLink, 
                            existingElement.GetDataChanges(newElement)));
                        result.ElementsToChange.Add(mapper.Map<Element>(newElement));
                    }
                }
                else if(existingElement == null)
                {
                    newElement.Id = elementId;
                    newElement.WorkspaceId = workspaceId;
                    await this.mediator.Publish(new ElementCreatedEvent(
                        newElement.WorkspaceId,
                        elementId,
                        import.Id,
                        newElement.Name,
                        import.Key,
                        import.SourceLink
                        ));
                    result.ElementsToAdd.Add(mapper.Map<Element>(newElement));
                }

                if(
                    newElement.DocumentationSections != null && newElement.DocumentationSections.Any() 
                    || newElement.DocumentationDecisions != null && newElement.DocumentationDecisions.Any()
                    )
                {
                    var elementDoc = new Documentation()
                    {
                        Id = Guid.NewGuid().ToString(),
                        WorkspaceId = workspaceId,
                        ElementId = elementId,
                        Sections = newElement.DocumentationSections != null ? 
                            this.mapper.Map<IEnumerable<DocumentationSection>>(newElement.DocumentationSections) : 
                            Enumerable.Empty<DocumentationSection>()
                    };

                    if(newElement.DocumentationImages != null)
                    {
                        var images = this.mapper.Map<IEnumerable<Image>>(newElement.DocumentationImages);
                        foreach (var image in images)
                        {
                            image.Id = Guid.NewGuid().ToString();
                            image.DocumentationId = elementDoc.Id;
                            image.WorkspaceId = workspaceId;
                        }
                        result.ImagesToAdd.AddRange(images);
                    }

                    if(newElement.DocumentationDecisions != null)
                    {
                        var decisions = this.DetectDecisionChnages(workspaceId, elementDoc.Id, elementId, newElement.DocumentationDecisions);
                        foreach(var decision in decisions)
                        {
                            decision.DocumentationId = elementDoc.Id;
                        }
                        result.DecisionsToAdd.AddRange(decisions);
                    }

                    result.DocumentationsToAdd.Add(elementDoc);
                    
                }
            }

            var elementsToDelete = existingElements
                .Where(oldElements =>
                    oldElements.Type == elementType &&
                    !newElements.Any(newElements => newElements.DslId == oldElements.DslId))
                .ToList();
            foreach (var elementToDelete in elementsToDelete)
            {
                await this.mediator.Publish(new ElementDeletedEvent(
                    elementToDelete.WorkspaceId,
                    elementToDelete.Id,
                    import.Id,
                    import.Key,
                    import.SourceLink,
                    elementToDelete.Name
                    ));
            }
            result.ElementsToDelete.AddRange(elementsToDelete);
            return result;
        }

        private ExplodedElement? GetBaseElementForInstance(ExplodedElement newElement, IEnumerable<ExplodedElement> newElements)
        {
            if (newElement.Type == ElementType.SoftwareSystemInstance)
            {
                return newElements
                    .FirstOrDefault(o => o.Type == ElementType.SoftwareSystem
                        && o.StructurizrId == newElement.StructurizrInstanceOfId);

            }else if(newElement.Type == ElementType.ContainerInstance)
            {
                return newElements
                    .FirstOrDefault(o => o.Type == ElementType.Container
                        && o.StructurizrId == newElement.StructurizrInstanceOfId);
            }
            return null;
        }

        private async Task<ElementsChangesResult> DetectSoftwareSystemChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.SoftwareSystem);
        }

        private async Task<ElementsChangesResult> DetectContainersChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.Container, ElementType.SoftwareSystem);
        }

        private async Task<ElementsChangesResult> DetectComponentsChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.Component, ElementType.Container);
        }

        private async Task<ElementsChangesResult> DetectPeopleChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.Person);
        }

        private async Task<ElementsChangesResult> DetectDeploymentNodesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.DeploymentNode, ElementType.DeploymentNode);
        }

        private async Task<ElementsChangesResult> DetectContainerInstancesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.ContainerInstance, ElementType.DeploymentNode);
        }

        private async Task<ElementsChangesResult> DetectSoftwareSystemInstancesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.SoftwareSystemInstance, ElementType.DeploymentNode);
        }

        private async Task<ElementsChangesResult> DetectInfrastructureNodesChanges(string workspaceId, WorkspaceImport import, IEnumerable<ExplodedElement> newElements, IEnumerable<Element> existingElements)
        {
            return await this.DetectChanges(workspaceId, import, newElements, existingElements, ElementType.InfrastructureNode, ElementType.DeploymentNode);
        }

    }
}
