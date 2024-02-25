using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectComponentsChangesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public async Task WhenThereIsNoChangesThenDetectorReturnsNoElementsChanged() {
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
        public async Task WhenThereIsNewComponentThenDetectorReturnsElementToAdd() {
            // Arrange
            this.explodedElements.Add(new ExplodedElement()
            {
                StructurizrId = "C02",
                StructurizrParentId = "C1",
                DslId = "C02",
                Name = "Component2",
                Type = ElementType.Component,
            });
            

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "C02").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C02 to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenThereAreChangesInComponentDataThenDetectorReturnsElementToChange() {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C01")
                .Name = "Component name changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "C01").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C01 to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }

        [Test]
        public async Task WhenDslIdOfComponentChangedThenDetectorReturnsElementToAddAndDelete() {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C01")
                .DslId = "C01Changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "C01Changed").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C01Changed to add in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "C01").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C01 to delete in detector results.");
        }
        [Test]
        public async Task WhenComponentHasBeenRemovedThenDetectorReturnsElementToDelete() {
            // Arrange
            var item = this.explodedElements.First(o => o.DslId == "C01");
            this.explodedElements.Remove(item);
            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "C01").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C01 to delete in detector results.");
        }
        [Test]
        public async Task WhenComponentChangedParentButDslIdNotChangedThenDetectorReturnsElementToChange() {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C01")
                .StructurizrParentId = "C2";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be no elements to delete in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "C01").Count(), Is.EqualTo(1), "There should be 1 element with dslid = C01 to change in detector results.");
        }

    }
}
