using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.RelationshipChanges
{
    public class DetectChangesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenRelationshipDoNotExistsThenRelationshipWillBeAdded()
        {
            // Arrange
            var relationshipsToImport = new List<RelationshipToImport>
            {
                new RelationshipToImport
                {
                    StructurizrId = "1",
                    DslId = "relationship1",
                    Description = "Description",
                    StructurizrDestinationId = "element2",
                    StructurizrSourceId = "element1",
                    Technology = "Technology"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "element1",
                    DslId = "element1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Element 1"
                },
                new Element
                {
                    Id = "testid2",
                    StructurizrId = "element2",
                    DslId = "element2",
                    Type = ElementType.SoftwareSystem,
                    Name = "Element 2"
                }
            };
            var existingRelationships = new List<Relationship>();
            var detector = new RelationshipsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectChanges("workspaceId", this.import, relationshipsToImport, existingRelationships, existingElements);

            // Assert
            Assert.That(existingRelationships.Count, Is.EqualTo(1));
            Assert.That(existingRelationships.First().SourceId, Is.EqualTo("testid1"));
            Assert.That(existingRelationships.First().DestinationId, Is.EqualTo("testid2"));
            Assert.That(existingRelationships.First().Description, Is.EqualTo("Description"));
            Assert.That(existingRelationships.First().Technology, Is.EqualTo("Technology"));
            Assert.That(existingRelationships.First().SourceName, Is.EqualTo("Element 1"));
            Assert.That(existingRelationships.First().DestinationName, Is.EqualTo("Element 2"));


            Assert.That(result.RelationshipsToAdd.Count, Is.EqualTo(1));
        }

        [Test]
        public async Task WhenLinkedRelationshipDoNotExistsThenRelationshipWillBeAdded()
        {
            // Arrange
            var relationshipsToImport = new List<RelationshipToImport>
            {
                new RelationshipToImport
                {
                    StructurizrId = "1",
                    DslId = "relationship1",
                    Description = "Description",
                    StructurizrDestinationId = "element2",
                    StructurizrSourceId = "element1",
                    Technology = "Technology"
                },
                new RelationshipToImport
                {
                    StructurizrId = "2",
                    StructurizrLinkedRelationshipId = "1",
                    DslId = "relationship1",
                    Description = "Description",
                    StructurizrDestinationId = "element2",
                    StructurizrSourceId = "element1",
                    Technology = "Technology"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "element1",
                    DslId = "element1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Element 1"
                },
                new Element
                {
                    Id = "testid2",
                    StructurizrId = "element2",
                    DslId = "element2",
                    Type = ElementType.SoftwareSystem,
                    Name = "Element 2"
                }
            };
            var existingRelationships = new List<Relationship>();
            var detector = new RelationshipsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectChanges("workspaceId", this.import, relationshipsToImport, existingRelationships, existingElements);

            // Assert
            Assert.That(existingRelationships.Count, Is.EqualTo(2));
            Assert.That(existingRelationships.First().SourceId, Is.EqualTo("testid1"));
            Assert.That(existingRelationships.First().DestinationId, Is.EqualTo("testid2"));
            Assert.That(existingRelationships.First().Description, Is.EqualTo("Description"));
            Assert.That(existingRelationships.First().Technology, Is.EqualTo("Technology"));
            Assert.That(existingRelationships.First().SourceName, Is.EqualTo("Element 1"));
            Assert.That(existingRelationships.First().DestinationName, Is.EqualTo("Element 2"));

            Assert.That(existingRelationships.Last().SourceId, Is.EqualTo("testid1"));
            Assert.That(existingRelationships.Last().DestinationId, Is.EqualTo("testid2"));
            Assert.That(existingRelationships.Last().Description, Is.EqualTo("Description"));
            Assert.That(existingRelationships.Last().Technology, Is.EqualTo("Technology"));
            Assert.That(existingRelationships.Last().SourceName, Is.EqualTo("Element 1"));
            Assert.That(existingRelationships.Last().DestinationName, Is.EqualTo("Element 2"));
            Assert.That(existingRelationships.Last().LinkedRelationshipId, Is.EqualTo(existingRelationships.First().Id));


            Assert.That(result.RelationshipsToAdd.Count, Is.EqualTo(2));
        }



    }
}
