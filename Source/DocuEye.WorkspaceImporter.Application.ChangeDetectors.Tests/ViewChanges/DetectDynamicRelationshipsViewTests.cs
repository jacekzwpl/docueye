using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectDynamicRelationshipsViewTests : BaseDetectorsTests
    {
        [Test]
        public void WhenDynamicRelationshipInViewIsDefindedThenAllProperitesAreMached()
        {
            // Arrange
            var relationshipsToImport = new List<RelationshipInViewToImport>()
            {
                new RelationshipInViewToImport()
                {
                    StructurizrId = "1",
                    Description = "Description",
                    Response = false,
                    Order = "1.0"
                }
            };
            var existingRelationships = new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "relation1",
                    StructurizrId = "1",
                    DslId = "dsl1",
                    InteractionStyle = "Synchronous",
                    Url = "https://www.docueye.com",
                    Technology = "REST",
                    Description = "Description",
                    Tags = new List<string>() { "Tag1", "Tag2" },
                    SourceId = "source1",
                    DestinationId = "destination1"
                    
                }
            };
            var detector = new ViewsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = detector.DetectDynamicRelationshipsView(relationshipsToImport, existingRelationships);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().BaseRelationshipId, Is.EqualTo("relation1"));
            Assert.That(result.First().Response, Is.EqualTo(false));
            Assert.That(result.First().Order, Is.EqualTo("1.0"));
            Assert.That(result.First().SourceId, Is.EqualTo("source1"));
            Assert.That(result.First().DestinationId, Is.EqualTo("destination1"));


        }

        [Test]
        public void WhenDynamicRelationshipInViewIsResponseThenAllProperitesAreMached()
        {
            // Arrange
            var relationshipsToImport = new List<RelationshipInViewToImport>()
            {
                new RelationshipInViewToImport()
                {
                    StructurizrId = "1",
                    Description = "Description",
                    Response = true,
                    Order = "1.0"
                }
            };
            var existingRelationships = new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "relation1",
                    StructurizrId = "1",
                    DslId = "dsl1",
                    InteractionStyle = "Synchronous",
                    Url = "https://www.docueye.com",
                    Technology = "REST",
                    Description = "Description",
                    Tags = new List<string>() { "Tag1", "Tag2" },
                    SourceId = "source1",
                    DestinationId = "destination1"

                }
            };
            var detector = new ViewsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = detector.DetectDynamicRelationshipsView(relationshipsToImport, existingRelationships);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().BaseRelationshipId, Is.EqualTo("relation1"));
            Assert.That(result.First().Response, Is.EqualTo(true));
            Assert.That(result.First().Order, Is.EqualTo("1.0"));
            Assert.That(result.First().DestinationId, Is.EqualTo("source1"));
            Assert.That(result.First().SourceId, Is.EqualTo("destination1"));
        }
    }
}
