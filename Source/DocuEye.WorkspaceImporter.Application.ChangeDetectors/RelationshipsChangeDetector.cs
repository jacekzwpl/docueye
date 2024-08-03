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
    public class RelationshipsChangeDetector
    {
        private readonly IMapper mapper;
        private readonly IMediator mediator;

        public RelationshipsChangeDetector(IMapper mapper, IMediator mediator)
        {
            this.mapper = mapper;
            this.mediator = mediator;
        }

        public async Task<RelationshipsChangeDetectorResult> DetectChanges(string workspaceId, WorkspaceImport import, IEnumerable<RelationshipToImport> relationshipsToImport, ICollection<Relationship> existingRelationships, IEnumerable<Element> existingElements)
        {
            var result = new RelationshipsChangeDetectorResult();

            //First process relationships without link
            foreach(var relationshipToImport in relationshipsToImport.Where(o => string.IsNullOrEmpty(o.StructurizrLinkedRelationshipId)))
            {
                // Find elements for relationship
                var (sourceElement, destinationElement) = this.FindElementsForRelationship(existingElements, relationshipToImport);
                if (sourceElement == null || destinationElement == null)
                {
                    continue;
                }
                // Check if relationship already exists
                var existingRelationship = existingRelationships
                    .FirstOrDefault(o => o.DslId == relationshipToImport.DslId);

                if (existingRelationship == null)
                {
                    var newRelationship = this.mapper.Map<Relationship>(relationshipToImport);
                    newRelationship.Id = Guid.NewGuid().ToString();
                    newRelationship.WorkspaceId = workspaceId;
                    newRelationship.SourceId = sourceElement.Id;
                    newRelationship.DestinationId = destinationElement.Id;
                    newRelationship.SourceName = sourceElement.Name;
                    newRelationship.DestinationName = destinationElement.Name;
                    existingRelationships.Add(newRelationship);

                    await this.mediator.Publish(new RelationshipCreatedEvent(
                        workspaceId,
                        newRelationship.Id,
                        import.Id,
                        import.Key,
                        import.SourceLink,
                        sourceElement.Name,
                        destinationElement.Name
                        ));

                    result.RelationshipsToAdd.Add(newRelationship.Id);
                }
                else
                {
                    var oldRelationship = existingRelationship.Clone();
                    this.mapper.Map<RelationshipToImport, Relationship>(relationshipToImport, existingRelationship);
                    existingRelationship.SourceId = sourceElement.Id;
                    existingRelationship.DestinationId = destinationElement.Id;
                    existingRelationship.SourceName = sourceElement.Name;
                    existingRelationship.DestinationName = destinationElement.Name;
                    if(!existingRelationship.IsDataEqual(oldRelationship))
                    {
                        await this.mediator.Publish(new RelationshipChangedEvent(
                            workspaceId,
                            existingRelationship.Id,
                            import.Id,
                            import.Key,
                            import.SourceLink,
                            sourceElement.Name,
                            destinationElement.Name,
                            existingRelationship.GetDataChanges(oldRelationship)
                            ));
                    }
                    result.RelationshipsToChange.Add(existingRelationship.Id);
                }
            }

            //Then process relationships with link
            foreach (var relationshipToImport in relationshipsToImport.Where(o => !string.IsNullOrEmpty(o.StructurizrLinkedRelationshipId)))
            {
                
                // Find elements for relationship
                var (sourceElement, destinationElement) = this.FindElementsForRelationship(existingElements, relationshipToImport);
                if (sourceElement == null || destinationElement == null)
                {
                    continue;
                }

                // Find source relationship
                var linkedRelationShip = existingRelationships
                       .FirstOrDefault(o => o.StructurizrId == relationshipToImport.StructurizrLinkedRelationshipId);

                if (linkedRelationShip != null)
                {
                    var newRelationship = this.mapper.Map<Relationship>(relationshipToImport);
                    newRelationship.Id = Guid.NewGuid().ToString();
                    newRelationship.SourceId = sourceElement.Id;
                    newRelationship.DestinationId = destinationElement.Id;
                    newRelationship.WorkspaceId = workspaceId;
                    newRelationship.LinkedRelationshipId = linkedRelationShip.Id;
                    newRelationship.SourceName = sourceElement.Name;
                    newRelationship.DestinationName = destinationElement.Name;

                    existingRelationships.Add(newRelationship);
                    result.RelationshipsToAdd.Add(newRelationship.Id);
                }
            }

            var relationshipsToDelete = existingRelationships
                .Where(oldRelationships =>
                    !relationshipsToImport.Any(newRelationships => newRelationships.DslId == oldRelationships.DslId))
                .ToList();
            result.RelationshipsToDelete.AddRange(relationshipsToDelete.Select(o => o.Id));

            foreach (var relationshipToDelete in relationshipsToDelete)
            {
                var exitstingRelationshipToDelete = existingRelationships
                    .FirstOrDefault(o => o.Id == relationshipToDelete.Id);
                if(exitstingRelationshipToDelete == null)
                {
                    continue;
                }
                //Send delete event
                await this.mediator.Publish(new RelationshipDeletedEvent(
                    relationshipToDelete.WorkspaceId,
                    relationshipToDelete.Id,
                    import.Id,
                    import.Key,
                    import.SourceLink,
                    exitstingRelationshipToDelete.SourceName,
                    exitstingRelationshipToDelete.DestinationName
                    ));
                //Remove from existing relationships
                existingRelationships.Remove(exitstingRelationshipToDelete);
            }

            return result;
        }

        private (Element sourceElement, Element destinationElement) FindElementsForRelationship(IEnumerable<Element> existingElements, RelationshipToImport relationshipToImport)
        {
            var sourceElement = existingElements
                .FirstOrDefault(o => o.StructurizrId == relationshipToImport.StructurizrSourceId);
            var destinationElement = existingElements
                .FirstOrDefault(o => o.StructurizrId == relationshipToImport.StructurizrDestinationId);
            return (sourceElement, destinationElement);
        }
    }
}
