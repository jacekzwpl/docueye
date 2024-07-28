using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Application.Events;
using DocuEye.WorkspaceImporter.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                }
                else
                {
                    this.mapper.Map<RelationshipToImport, Relationship>(relationshipToImport, existingRelationship);
                    existingRelationship.SourceId = sourceElement.Id;
                    existingRelationship.DestinationId = destinationElement.Id;
                }
            }

            //Then process relationships with link
            foreach (var relationshipToImport in relationshipsToImport.Where(o => !string.IsNullOrEmpty(o.StructurizrLinkedRelationshipId)))
            {
                var linkedRelationShip = existingRelationships
                        .FirstOrDefault(o => o.StructurizrId == relationshipToImport.StructurizrLinkedRelationshipId);

                // Find elements for relationship
                var (sourceElement, destinationElement) = this.FindElementsForRelationship(existingElements, relationshipToImport);
                if (sourceElement == null || destinationElement == null)
                {
                    continue;
                }

                if (linkedRelationShip != null)
                {
                    var newRelationship = this.mapper.Map<Relationship>(relationshipToImport);
                    newRelationship.Id = Guid.NewGuid().ToString();
                    newRelationship.SourceId = sourceElement.Id;
                    newRelationship.DestinationId = destinationElement.Id;
                    newRelationship.WorkspaceId = workspaceId;
                    newRelationship.LinkedRelationshipId = linkedRelationShip.Id;
                    newRelationship.DslId = Guid.NewGuid().ToString();
                }

                
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
