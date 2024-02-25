using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectDeploymentNodesChangesTests : BaseWorkspaceChangeDetectorTests
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
        public async Task WhenThereIsNewDeploymentNodeThenDetectorReturnsElementToAdd()
        {
            // Arrange
            this.explodedElements.Add(new ExplodedElement()
            {
                StructurizrId = "N3",
                DslId = "N3",
                Name = "Node3",
                Type = ElementType.DeploymentNode,
            });

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "N3").Count(), Is.EqualTo(1), "There should be 1 element with dslid = N3 to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenThereAreChangesInDeploymentNodeDataThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "N1")
                .Name = "Name changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "N1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = N1 to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenDslIdOfDeploymentNodeChangedThenDetectorReturnsElementToAddAndDelete()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "N1_1")
                .DslId = "N1_1Changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "N1_1Changed").Count(), Is.EqualTo(1), "There should be 1 element with dslid = N1_1Changed to add in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "N1_1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = N1_1 to delete in detector results.");
        }
        [Test]
        public async Task WhenDeploymentNodeHasBeenRemovedThenDetectorReturnsElementToDelete()
        {
            // Arrange
            var item = this.explodedElements.First(o => o.DslId == "N1_1");
            this.explodedElements.Remove(item);

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should be 1 element to delete in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "N1_1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = N1_1 to delete in detector results.");
        }
        [Test]
        public async Task WhenDeploymentNodeChangedParentButDslIdNotChangedThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "N1_1")
                .StructurizrParentId = "N2";


            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements, this.existingDecisions);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be no elements to delete in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "N1_1").Count(), Is.EqualTo(1), "There should be 1 element with dslid = N1_1 to change in detector results.");
        }
    }
}
