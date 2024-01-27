using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectContainerInstanceChangesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public async Task WhenThereIsNoChangesThenDetectorReturnsNoElementsChanged()
        {
            // Arrange

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be any elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenThereIsNewInstanceThenDetectorReturnsElementToAdd()
        {
            // Arrange
            this.explodedElements.Add(new ExplodedElement()
            {
                StructurizrId = "CI2",
                StructurizrInstanceOfId = "C1",
                StructurizrParentId = "N1",
                DslId = "CI2",
                Type = ElementType.ContainerInstance,
            });

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "CI2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI2 to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenThereAreChangesInInstanceDataThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "C1")
                .Name = "Name changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(2), "There should not be 2 element to change in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "CI1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI1 to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenDslIdOfInstanceChangedThenDetectorReturnsElementToAddAndDelete()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "CI1")
                .DslId = "CI1Chnaged";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "CI1Chnaged").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI1Chnaged to add in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "CI1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI1 to delete in detector results.");
        }
        [Test]
        public async Task WhenInstanceHasBeenRemovedThenDetectorReturnsElementToDelete()
        {
            // Arrange
            var item = this.explodedElements.First(o => o.DslId == "CI1");
            this.explodedElements.Remove(item);

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "CI1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI1 to delete in detector results.");
        }
        [Test]
        public async Task WhenInstanceChangedParentButDslIdNotChangedThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "CI1")
                .StructurizrParentId = "N2";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be no elements to delete in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "CI1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI1 to change in detector results.");
        }
        [Test]
        public async Task WhenInstanceChangedInstanceOfButDslIdNotChangedThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "CI1")
                .StructurizrInstanceOfId = "C2";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be no elements to delete in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "CI1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = CI1 to change in detector results.");
        }
    }
}
