using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectElementsInViewTests : BaseDetectorsTests
    {
        [Test]
        public void WhenElementInViewIsDefindedThenAllPropertiesAreMatched()
        {
            var elementsInView = new List<ElementInViewToImport>()
            {
                new ElementInViewToImport()
                {
                    StructurizrId = "1",
                    X = 1,
                    Y = 2
                }
            };
            var existingElements = new List<Element>()
            {
                new Element()
                {
                    Id = "element1",
                    StructurizrId = "1",
                    Name = "Element1",
                    Description = "Description",
                    Type = "Type",
                    Technology = "Technology",
                    Url = "https://www.docueye.com",
                    Tags = new List<string>() { "Tag1", "Tag2" }
                }
            };
            var elementsDiagrams = new Dictionary<string, string>()
            {
                { "element1", "viewid1" }
            };
            var detector = new ViewsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = detector.DetectElementsInView(elementsInView, existingElements, elementsDiagrams);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("element1"));
            Assert.That(result.First().Name, Is.EqualTo("Element1"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().Type, Is.EqualTo("Type"));
            Assert.That(result.First().Technology, Is.EqualTo("Technology"));
            Assert.That(result.First().Url, Is.EqualTo("https://www.docueye.com"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("Tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("Tag2"));
            Assert.That(result.First().X, Is.EqualTo(1));
            Assert.That(result.First().Y, Is.EqualTo(2));
            Assert.That(result.First().DiagramId, Is.EqualTo("viewid1"));

        }
    }
}
