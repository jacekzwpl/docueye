using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;

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
            var detector = new RelationshipsChangeDetector(this.mediator);

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
            var detector = new RelationshipsChangeDetector(this.mediator);

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

        [Test]
        public async Task WhenRelationshipChangedThenWillBeUpdated() { 
        
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
            var existingRelationships = new List<Relationship>
            {
                new Relationship
                {
                    Id = "testid1",
                    DslId = "relationship1",
                    SourceId = "testid1",
                    DestinationId = "testid2",
                    Description = "Old Description",
                    Technology = "Old Technology",
                    SourceName = "Old Element 1",
                    DestinationName = "Old Element 2"
                }
            };
            var detector = new RelationshipsChangeDetector(this.mediator);

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

            Assert.That(result.RelationshipsToChange.Count, Is.EqualTo(1));
            
        }

        [Test]
        public async Task WhenRelationshipChangedThenLinkedRelationshipIsAlsoHanged()
        {             // Arrange
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
            var existingRelationships = new List<Relationship>
            {
                new Relationship
                {
                    Id = "testid1",
                    DslId = "relationship1",
                    SourceId = "testid1",
                    DestinationId = "testid2",
                    Description = "Old Description",
                    Technology = "Old Technology",
                    SourceName = "Old Element 1",
                    DestinationName = "Old Element 2"
                },
                new Relationship
                {
                    Id = "testid2",
                    DslId = "oldrelationship1",
                    SourceId = "testid1",
                    DestinationId = "testid2",
                    Description = "Old Description",
                    Technology = "Old Technology",
                    SourceName = "Old Element 1",
                    DestinationName = "Old Element 2",
                    LinkedRelationshipId = "testid1"
                }
            };
            var detector = new RelationshipsChangeDetector(this.mediator);

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


            Assert.That(result.RelationshipsToChange.Count, Is.EqualTo(1));
            Assert.That(result.RelationshipsToAdd.Count, Is.EqualTo(1));
            Assert.That(result.RelationshipsToDelete.Count, Is.EqualTo(1));
            Assert.That(result.RelationshipsToDelete.First(), Is.EqualTo("testid2"));
        }

        [Test]
        public async Task WhenRelationshipHasBeenDeletedThenIsRemoved()
        {
            var relationshipsToImport = new List<RelationshipToImport>
            {

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

            var existingRelationships = new List<Relationship>
            {
                new Relationship
                {
                    Id = "testid1",
                    DslId = "relationship1",
                    SourceId = "testid1",
                    DestinationId = "testid2",
                    Description = "Description",
                    Technology = "Technology",
                    SourceName = "Element 1",
                    DestinationName = "Element 2"
                }
            };
            var detector = new RelationshipsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectChanges("workspaceId", this.import, relationshipsToImport, existingRelationships, existingElements);

            // Assert
            Assert.That(existingRelationships.Count, Is.EqualTo(0));
            Assert.That(result.RelationshipsToDelete.Count, Is.EqualTo(1));
            Assert.That(result.RelationshipsToDelete.First(), Is.EqualTo("testid1"));
        }

        [Test]
        public async Task WhenRelationShipHasBeenDeletedThenIsRemovedWithAllLinkedRelationships()
        {
            var relationshipsToImport = new List<RelationshipToImport>
            {

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

            var existingRelationships = new List<Relationship>
            {
                new Relationship
                {
                    Id = "testid1",
                    DslId = "relationship1",
                    SourceId = "testid1",
                    DestinationId = "testid2",
                    Description = "Description",
                    Technology = "Technology",
                    SourceName = "Element 1",
                    DestinationName = "Element 2"
                },
                new Relationship
                {
                    Id = "testid2",
                    DslId = "oldrelationship1",
                    SourceId = "testid1",
                    DestinationId = "testid2",
                    Description = "Description",
                    Technology = "Technology",
                    SourceName = "Element 1",
                    DestinationName = "Element 2",
                    LinkedRelationshipId = "testid1"
                }
            };
            var detector = new RelationshipsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectChanges("workspaceId", this.import, relationshipsToImport, existingRelationships, existingElements);

            // Assert
            Assert.That(existingRelationships.Count, Is.EqualTo(0));
            Assert.That(result.RelationshipsToDelete.Count, Is.EqualTo(2));
            Assert.That(result.RelationshipsToDelete.First(), Is.EqualTo("testid1"));
            Assert.That(result.RelationshipsToDelete.Last(), Is.EqualTo("testid2"));
        }

    }
}
