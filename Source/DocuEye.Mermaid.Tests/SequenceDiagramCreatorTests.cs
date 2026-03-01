using DocuEye.ViewsKeeper.Model;

namespace DocuEye.Mermaid.Tests
{
    public class SequenceDiagramCreatorTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateFromDynamicViewReturnsExpectedSequenceDiagram()
        {
            // Arrange
            var dynamicView = new DynamicView
            {
                Elements = new[]
                {
                    new ElementView { Id = "A", Name = "Element A" },
                    new ElementView { Id = "B", Name = "Element B" }
                },
                Relationships = new[]
                {
                    new DynamicRelationshipView { SourceId = "A", DestinationId = "B", Description = "calls" },
                    new DynamicRelationshipView { SourceId = "B", DestinationId = "A", Description = "returns", Response = true }
                }
            };
            var creator = new SequenceDiagramCreator();

            // Act
            var result = creator.Create(dynamicView);

            // Assert
            var expected = "sequenceDiagram\r\n\tparticipant A as Element A\r\n\tparticipant B as Element B\r\n\tA ->> B: calls\r\n\tB -->> A: returns";
            Assert.That(result.Trim(), Is.EqualTo(expected.Trim()));
        }
    }
}
