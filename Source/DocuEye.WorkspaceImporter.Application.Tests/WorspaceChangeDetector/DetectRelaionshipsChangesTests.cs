using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectRelaionshipsChangesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public async Task WhenThereIsNoChangesThenDetectorReturnsNoRelationshipsChanged()
        {
            // Arrange
            foreach(var element in this.explodedElements)
            {
                element.Id = "Id" + element.StructurizrId;
            }

            

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectRelaionshipsChanges("wId", this.import, this.explodedRelationships, this.existingRelationships, this.explodedElements);

            // Assert
            Assert.That(result.RelationshipsToAdd?.Count, Is.EqualTo(0), "There should not be any relationships to add in detector results.");
            Assert.That(result.RelationshipsToChange?.Count, Is.EqualTo(0), "There should not be any relationships to change in detector results.");
            Assert.That(result.RelationshipsToDelete?.Count, Is.EqualTo(0), "There should not be any relationships to delete in detector results.");
        }

        [Test]
        public async Task WhenThereIsNewRelationshipThenDetectorReturnsRelationshipToAdd()
        {
            // Arrange
            foreach (var element in this.explodedElements)
            {
                element.Id = "Id" + element.StructurizrId;
            }

            this.explodedRelationships.Add(new ExplodedRelationship()
            {
                StructurizrId = "R3",
                Description = "DescriptionR3",
                DslId = "R3",
                StructurizrSourceId = "C1",
                StructurizrDestinationId = "C2"
            });

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectRelaionshipsChanges("wId", this.import, this.explodedRelationships, this.existingRelationships, this.explodedElements);

            // Assert
            Assert.That(result.RelationshipsToAdd?.Count, Is.EqualTo(1), "There should be 1 relationship to add in detector results.");
            Assert.That(result.RelationshipsToChange?.Count, Is.EqualTo(0), "There should not be any relationships to change in detector results.");
            Assert.That(result.RelationshipsToDelete?.Count, Is.EqualTo(0), "There should not be any relationships to delete in detector results.");
        }

        [Test]
        public async Task WhenThereAreChnagesInRelationshipThenDetectorReturnsRelationshipsToChange()
        {
            // Arrange
            foreach (var element in this.explodedElements)
            {
                element.Id = "Id" + element.StructurizrId;
            }

            this.explodedRelationships.First(o => o.DslId == "R1")
                .Description = "Chnaged description";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectRelaionshipsChanges("wId", this.import, this.explodedRelationships, this.existingRelationships, this.explodedElements);

            // Assert
            Assert.That(result.RelationshipsToAdd?.Count, Is.EqualTo(0), "There should not be any relationships to add in detector results.");
            Assert.That(result.RelationshipsToChange?.Count, Is.EqualTo(1), "There should be 1 relationship to change in detector results.");
            Assert.That(result.RelationshipsToDelete?.Count, Is.EqualTo(0), "There should not be any relationships to delete in detector results.");

        }

        [Test]
        public async Task WhenDslIdOfRelationshipChnagedThenDetectorReturnsRelationshipToAddAndDelete()
        {
            // Arrange
            foreach (var element in this.explodedElements)
            {
                element.Id = "Id" + element.StructurizrId;
            }

            this.explodedRelationships.First(o => o.DslId == "R1")
                .DslId = "R1Changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectRelaionshipsChanges("wId", this.import, this.explodedRelationships, this.existingRelationships, this.explodedElements);

            // Assert
            Assert.That(result.RelationshipsToAdd?.Count, Is.EqualTo(1), "There should be 1 relationship to add in detector results.");
            Assert.That(result.RelationshipsToChange?.Count, Is.EqualTo(0), "There should not be any relationships to change in detector results.");
            Assert.That(result.RelationshipsToDelete?.Count, Is.EqualTo(1), "There should be 1 relationship to delete in detector results.");

        }

        [Test]
        public async Task WhenRelationshipWasRemovedThenDetectorReturnsItInRelationshipsToDelete()
        {
            // Arrange
            foreach (var element in this.explodedElements)
            {
                element.Id = "Id" + element.StructurizrId;
            }

            var item = this.explodedRelationships.First(o => o.DslId == "R1");
            this.explodedRelationships.Remove(item);

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectRelaionshipsChanges("wId", this.import, this.explodedRelationships, this.existingRelationships, this.explodedElements);

            // Assert
            Assert.That(result.RelationshipsToAdd?.Count, Is.EqualTo(0), "There should not be any relationships to add in detector results.");
            Assert.That(result.RelationshipsToChange?.Count, Is.EqualTo(0), "There should not be any relationships to change in detector results.");
            Assert.That(result.RelationshipsToDelete?.Count, Is.EqualTo(1), "There should be 1 relationship to delete in detector results.");

        }
    }
}
