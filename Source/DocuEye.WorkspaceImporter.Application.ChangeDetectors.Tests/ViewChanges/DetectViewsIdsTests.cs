using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectViewsIdsTests : BaseDetectorsTests
    {
        [Test]
        public void WhenNewViewIsDefindedThenIdIsGenerated() { 
        
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1"
                }
            };
            var existingViews = new List<BaseView>();
            var existingElements = new List<Element>();
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var (viewsIdsMap, elementsDiagrams) = detector.DetectViewsIds(viewsToImport, existingViews, existingElements);

            // Assert
            Assert.That(viewsIdsMap.Count, Is.EqualTo(1));
            Assert.That(viewsIdsMap.First().Key, Is.EqualTo("view1"));
        
        }

        [Test]
        public void WhenExistingViewIsDefindedThenIdIsTakenFromExistingView() { 
        
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1"
                }
            };
            var existingViews = new List<BaseView>()
            {
                new BaseView()
                {
                    Id = "viewid1",
                    Key = "view1"
                }
            };
            var existingElements = new List<Element>();
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var (viewsIdsMap, elementsDiagrams) = detector.DetectViewsIds(viewsToImport, existingViews, existingElements);

            // Assert
            Assert.That(viewsIdsMap.Count, Is.EqualTo(1));
            Assert.That(viewsIdsMap.First().Key, Is.EqualTo("view1"));
            Assert.That(viewsIdsMap.First().Value, Is.EqualTo("viewid1"));
        }

        [Test]
        public void WhenNewViewHasContextElementThenElementIsAddedToElementsDiagrams() { 
            
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    StructurizrElementId = "element1",
                    ViewType = ViewType.ContainerView
                }
            };
            var existingViews = new List<BaseView>();
            var existingElements = new List<Element>()
            {
                new Element()
                {
                    Id = "element1",
                    StructurizrId = "element1",
                    Type = ElementType.SoftwareSystem
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var (viewsIdsMap, elementsDiagrams) = detector.DetectViewsIds(viewsToImport, existingViews, existingElements);

            // Assert
            Assert.That(elementsDiagrams.Count, Is.EqualTo(1));
            Assert.That(elementsDiagrams.First().Key, Is.EqualTo("element1"));
            Assert.That(elementsDiagrams.First().Value, Is.EqualTo(viewsIdsMap["view1"]));
        }

        [Test]
        public void WhenExistingViewHasContextElementThenElementIsAddedToElementsDiagrams() { 
        
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    StructurizrElementId = "element1",
                    ViewType = ViewType.ContainerView
                }
            };
            var existingViews = new List<BaseView>() {
                new BaseView()
                {
                    Id = "viewid1",
                    Key = "view1",
                    ViewType = ViewType.ContainerView
                }
            };
            var existingElements = new List<Element>()
            {
                new Element()
                {
                    Id = "ID",
                    StructurizrId = "element1",
                    Type = ElementType.SoftwareSystem
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var (viewsIdsMap, elementsDiagrams) = detector.DetectViewsIds(viewsToImport, existingViews, existingElements);

            // Assert
            Assert.That(elementsDiagrams.Count, Is.EqualTo(1));
            Assert.That(elementsDiagrams.First().Key, Is.EqualTo("ID"));
            Assert.That(elementsDiagrams.First().Value, Is.EqualTo("viewid1"));
        }
    }
}
