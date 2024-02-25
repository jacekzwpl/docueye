using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectContainersChangesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public async Task WhenThereIsNoChangesThenDetectorReturnsNoElementsChanged()
        {
            // Arrange

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be any elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }

        [Test]
        public async Task WhenThereIsNewContainerThenDetectorReturnsElementToAdd()
        {
            // Arrange
            this.explodedElements.Add(new ExplodedElement()
            {
                StructurizrId = "C3",
                StructurizrParentId = "S1",
                DslId = "C3",
                Name = "Container3",
                Type = ElementType.Container,
            });

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should be 1 element to add in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "C3").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C3 to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }

        [Test]
        public async Task WhenThereAreChangesInContainerDataThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C2")
                .Name = "Container name changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should be 1 element to change in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "C2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C2 to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }

        [Test]
        public async Task WhenDslIdOfContainerChangedThenDetectorReturnsElementToAddAndDelete()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C2")
                .DslId = "C2Changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should be 1 element to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should be 1 element to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "C2Changed").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C2Changed to add in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "C2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C2 to delete in detector results.");
        }

        [Test]
        public async Task WhenContainerHasBeenRemovedThenDetectorReturnsElementToDelete()
        {
            // Arrange
            var item = this.explodedElements.First(o => o.DslId == "C2");
            this.explodedElements.Remove(item);

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should be 1 element to delete in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "C2").Count(), Is.EqualTo(1), "There should be 2 element with dslid = C1 to delete in detector results.");
        }

        [Test]
        public async Task WhenContainerChangedParentButDslIdNotChangedThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C2")
                .StructurizrParentId = "S2";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should be 1 element to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should be no elements to delete in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "C2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C2 to change in detector results.");
        }
    }
}
