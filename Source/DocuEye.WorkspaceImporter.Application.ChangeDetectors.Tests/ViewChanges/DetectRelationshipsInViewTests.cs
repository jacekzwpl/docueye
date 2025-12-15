using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectRelationshipsInViewTests : BaseDetectorsTests
    {
        [Test]
        public void WhenRelationshipInViewIsDefindedThenAllProperitesAreMached()
        {
            // Arrange
            var relationshipsToImport = new List<RelationshipInViewToImport>()
            {
                new RelationshipInViewToImport()
                {
                    StructurizrId = "1"
                }
            };
            var existingRelationships = new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "relation1",
                    StructurizrId = "1",
                    DslId = "dsl1",
                    InteractionStyle = "Synchronous",
                    Url = "https://www.docueye.com",
                    Technology = "REST",
                    Description = "Description",
                    Tags = new List<string>() { "Tag1", "Tag2" }
                }
            };
            var detector = new ViewsChangeDetector(this.mediator);

            // Act
            var result = detector.DetectRelationshipsInView(relationshipsToImport, existingRelationships);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("relation1"));
            Assert.That(result.First().DslId, Is.EqualTo("dsl1"));
            Assert.That(result.First().InteractionStyle, Is.EqualTo("Synchronous"));
            Assert.That(result.First().Url, Is.EqualTo("https://www.docueye.com"));
            Assert.That(result.First().Technology, Is.EqualTo("REST"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("Tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("Tag2"));

        }

    }
}
