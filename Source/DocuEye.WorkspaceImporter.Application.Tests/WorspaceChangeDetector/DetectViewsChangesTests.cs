using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectViewsChangesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public void WhenDetectViewChangesThenAllViewsAreMarkedToAdd()
        {
            // Arrange
            
            
            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = detector.DetectViewsChanges("wId", this.explodedViewsResult, this.explodedElements, this.explodedRelationships);

            // Assert
            Assert.That(result.SystemLandscapeViewsToAdd.Count, Is.EqualTo(1), "There should be 1 SystemLandscapeView to add.");
            Assert.That(result.SystemContextViewsToAdd.Count, Is.EqualTo(1), "There should be 1 SystemContextView to add.");
            Assert.That(result.ContainerViewsToAdd.Count, Is.EqualTo(1), "There should be 1 ContainerView to add.");
            Assert.That(result.ComponentViewsToAdd.Count, Is.EqualTo(1), "There should be 1 ComponentView to add.");
            Assert.That(result.DynamicViewsToAdd.Count, Is.EqualTo(1), "There should be 1 DynamicView to add.");
            Assert.That(result.DeploymentViewsToAdd.Count, Is.EqualTo(1), "There should be 1 DeploymentView to add.");
            Assert.That(result.ImagesViewsToAdd.Count, Is.EqualTo(1), "There should be 1 ImagesView to add.");

        }
    }
}
