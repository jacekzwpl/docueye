using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectImageViewsTests : BaseDetectorsTests
    {
        [Test]
        public void WhenImageViewIsDefindedThenAllPropertiesAreMatched()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.ImageView,
                    Content = "Content",
                    ContentType = "ContentType",
                    StructurizrElementId = "1",
                    Title = "Title",
                    Description = "Description"
                }
            };
            var viewsIdsMap = new Dictionary<string, string>()
            {
                { "view1", "viewid1" }
            };
            var existingElements = new List<Element>()
            {
                new Element()
                {
                    Id = "elementId1",
                    StructurizrId = "1"
                }
                
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var result = detector.DetectImageViews(
                "workspaceId",
                viewsToImport,
                viewsIdsMap,
                existingElements);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Key, Is.EqualTo("view1"));
            Assert.That(result.First().ViewType, Is.EqualTo(ViewType.ImageView));
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().Content, Is.EqualTo("Content"));
            Assert.That(result.First().ContentType, Is.EqualTo("ContentType"));
            Assert.That(result.First().ElementId, Is.EqualTo("elementId1"));
        }
    }
}
