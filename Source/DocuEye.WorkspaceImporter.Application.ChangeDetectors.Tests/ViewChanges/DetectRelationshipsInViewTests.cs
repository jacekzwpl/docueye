using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectRelationshipsInViewTests : BaseDetectorsTests
    {
        [Test]
        public void WhenRelationshipIsDefindedThenAllProperitesAreMached()
        {
            // Arrange
            var workspaceId = Guid.NewGuid().ToString();
            var relationshipsToImport = new List<RelationshipToImport>()
            {
                new RelationshipToImport()
                {
                    DslId = "1",
                    InteractionStyle = "Synchronous",
                    Url = "http://www.google.com",
                    Technology = "REST",
                    Description = "Description",
                    Tags = new List<string>() { "Tag1", "Tag2" }
                }
            };
            var existingRelationships = new List<Relationship>()
            {
                new Relationship()
                {
                    DslId = "1",
                    InteractionStyle = "Synchronous",
                    Url = "http://www.google.com",
                    Technology = "REST",
                    Description = "Description",
                    Tags = new List<string>() { "Tag1", "Tag2" }
                }
            };
            var existingElements = new List<Element>()
            {
                new Element()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Element1",
                    StructurizrId = "1"
                },
                new Element()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = "Element2",
                    StructurizrId = "2"
                }
            };
            var detector = new ViewsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = detector.DetectRelationshipsInView(relationshipsToImport, existingRelationships);
        }

    }
}
