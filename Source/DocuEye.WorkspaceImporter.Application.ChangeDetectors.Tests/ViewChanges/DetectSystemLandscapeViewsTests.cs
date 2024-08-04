using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectSystemLandscapeViewsTests : BaseDetectorsTests
    {
        [Test]
        public void WhenSystemLandscapeViewIsDefindedThenAllPropertiesAreMatched()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.SystemLandscapeView,
                    Elements = new List<ElementInViewToImport>()
                    {
                        new ElementInViewToImport()
                        {
                            StructurizrId = "1",
                            X = 1,
                            Y = 2
                        },
                        new ElementInViewToImport()
                        {
                            StructurizrId = "2",
                            X = 3,
                            Y = 4
                        }
                    },
                    Relationships = new List<RelationshipInViewToImport>()
                    {
                        new RelationshipInViewToImport()
                        {
                            StructurizrId = "1"
                        }
                    },
                    PaperSize = "A4",
                    ExternalBoundariesVisible = true,
                    Title = "Title",
                    Tags = new List<string>() { "Tag1", "Tag2" },
                    Description = "Description",
                    AutomaticLayout = new ViewAutomaticLayout()
                    {
                        RankDirection = "TopBottom"
                    }
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
                    Id = "element1",
                    StructurizrId = "1"
                },
                new Element()
                {
                    Id = "element2",
                    StructurizrId = "2"
                }
            };
            var existingRelationships = new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "relation1",
                    StructurizrId = "1"
                }
            };
            var elementDiagrams = new Dictionary<string, string>();
            var detector = new ViewsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = detector.DetectSystemLandscapeViews(
                "workspaceId",
                viewsToImport, 
                viewsIdsMap, 
                existingElements, 
                existingRelationships, 
                elementDiagrams);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("viewid1"));
            Assert.That(result.First().WorkspaceId, Is.EqualTo("workspaceId"));
            Assert.That(result.First().Elements.Count, Is.EqualTo(2));
            Assert.That(result.First().Relationships.Count, Is.EqualTo(1));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().EnterpriseBoundaryVisible, Is.True);
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));


        }
    }
}
