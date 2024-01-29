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
            Assert.That(result.FilteredViewsToAdd.Count, Is.EqualTo(1), "There should be 1 FilteredView to add.");

        }

        [Test]
        public void WhenDetectViewChangesWithFilteredViewIncludeModeThenFilteredViewContainsOnlySelectedItems()
        {
            // Arrange


            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = detector.DetectViewsChanges("wId", this.explodedViewsResult, this.explodedElements, this.explodedRelationships);

            // Assert
            Assert.That(result.FilteredViewsToAdd.Count, Is.EqualTo(1), "There should be 1 FilteredView to add.");
            Assert.That(result.FilteredViewsToAdd[0].Elements.Any(o => o.Tags != null && !o.Tags.Contains("Test")), Is.EqualTo(false), "There should be no elements that do not contain tag Test.");
            Assert.That(result.FilteredViewsToAdd[0].Relationships.Any(o => o.Tags != null && !o.Tags.Contains("Test")), Is.EqualTo(false), "There should be no realtionships that do not contain tag Test.");
        }

        [Test]
        public void WhenDetectViewChangesWithFilteredViewExcludeModeThenFilteredViewContainsOnlySelectedItems()
        {
            // Arrange
            var view  = this.explodedViewsResult.FilteredViews.First(o => o.Key == "Filtered1");
            view.Mode = "Exclude";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = detector.DetectViewsChanges("wId", this.explodedViewsResult, this.explodedElements, this.explodedRelationships);

            // Assert
            Assert.That(result.FilteredViewsToAdd.Count, Is.EqualTo(1), "There should be 1 FilteredView to add.");
            Assert.That(result.FilteredViewsToAdd[0].Elements.Count(o => o.Tags != null && !o.Tags.Contains("Test")), Is.EqualTo(0), "There should be no elements that do contain tag Test.");
            Assert.That(result.FilteredViewsToAdd[0].Relationships.Count(o => o.Tags != null && !o.Tags.Contains("Test")), Is.EqualTo(0), "There should be no realtionships that do contain tag Test.");
        }

        [Test]
        public void WhenDetectViewChangesWithFilteredViewExcludeAllModeThenFilteredViewDoesNotContainAnyItems()
        {
            // Arrange
            var view = this.explodedViewsResult.FilteredViews.First(o => o.Key == "Filtered1");
            view.Mode = "Exclude";
            view.Tags = new string[] { "*" };

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = detector.DetectViewsChanges("wId", this.explodedViewsResult, this.explodedElements, this.explodedRelationships);

            // Assert
            Assert.That(result.FilteredViewsToAdd.Count, Is.EqualTo(1), "There should be 1 FilteredView to add.");
            Assert.That(result.FilteredViewsToAdd[0].Elements.Count(), Is.EqualTo(0), "There should be no elements in view");
            Assert.That(result.FilteredViewsToAdd[0].Relationships.Count(), Is.EqualTo(0), "There should be no realtionships in view.");
        }

        [Test]
        public void WhenDetectViewChangesWithFilteredViewIncludeAllModeThenFilteredViewDoesContainsAllSourceViewItems()
        {
            // Arrange
            var view = this.explodedViewsResult.FilteredViews.First(o => o.Key == "Filtered1");
            view.Tags = new string[] { "*" };

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = detector.DetectViewsChanges("wId", this.explodedViewsResult, this.explodedElements, this.explodedRelationships);

            // Assert
            var sourceView = result.SystemContextViewsToAdd.First(o => o.Key == "SystemContext1");

            Assert.That(result.FilteredViewsToAdd.Count, Is.EqualTo(1), "There should be 1 FilteredView to add.");
            Assert.That(result.FilteredViewsToAdd[0].Elements.Count(), Is.EqualTo(sourceView.Elements.Count()), "Source and filtered view should have same number of elements.");
            Assert.That(result.FilteredViewsToAdd[0].Relationships.Count(), Is.EqualTo(sourceView.Relationships.Count()), "Source and filtered view should have same number of relationships.");
        }
    }
}
