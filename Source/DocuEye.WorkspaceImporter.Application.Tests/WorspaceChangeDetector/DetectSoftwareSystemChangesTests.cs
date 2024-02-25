using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectSoftwareSystemChangesTests : BaseWorkspaceChangeDetectorTests
    {
        
        [Test]
        public async Task WhenThereIsNoChnagesInSystemsThenDetectorReturnsNoElements()
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
        public async Task WhenThereIsOneNewSystemThenDetectorReturnsItInElementsToAdd()
        {
            // Arrange

            this.explodedElements.Add(new ExplodedElement()
            {
                StructurizrId = "S3",
                DslId = "S3",
                Name = "System3",
                Type = ElementType.SoftwareSystem,
            });
            


            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should be 1 element to add in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "S3").Count(), Is.EqualTo(1), "There should be 1 element with dslid = S3 to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }

        [Test]
        public async Task WhenThereAreChnagesInSystemsThenDetectorReturnsElementsToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "S2")
                .Name = "System2 name changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should be 1 element to change in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "S2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = S2 to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be any elements to add in detector results.");
        }

        [Test]
        public async Task WhenDslIdOfSystemChnagedThenDetectorReturnsElementToAddAndDelete()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "S2")
                .DslId = "S2Changed";
            
            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should be no elements to change in detector results.");
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should be 1 element to add in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should be 1 element to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "S2Changed").Count(), Is.EqualTo(1), "There should be 1 element with dslid = S2Changed to add in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "S2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = S2 to delete in detector results.");
        }

        [Test]
        public async Task WhenSystemWasRemovedThenDetectorReturnsItInElementsToDelete()
        {
            // Arrange
            var item = this.explodedElements.First(o => o.DslId == "S2");
            this.explodedElements.Remove(item);

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should be no elements to change in detector results.");
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should be no elements to add in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should be 1 element to delete in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "S2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = S2 to delete in detector results.");
        }
    }
}
