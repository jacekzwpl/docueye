using DocuEye.Infrastructure.Tests.Common;
using DocuEye.Linter.Model;
using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.DSL.Model.Maps.Tests
{
    public class StructurizrRelationshipMapTests
    {
        [Test]
        public void Mapping_StructurizrRelationship_To_LinterModelRelationship_Works()
        {
            // Arrange
            var elements = new List<StructurizrModelElement>
            {
                new StructurizrModelElement("model-1", "element-1") { Name = "Element 1" },
                new StructurizrModelElement("model-1", "element-2") { Name = "Element 2" }
            };
            var source = new StructurizrRelationship("model-1",new string[] {"element-1", "element-2"})
            {
                ModelId = "modelid-1",

                SourceIdentifier = "element-1",
                DestinationIdentifier = "element-2",
                Technology = "HTTP",
                Description = "Communicates with",
                Properties = new Dictionary<string, string>
                {
                    { DslPropertyNames.DslIdProperty, "ValueA" },
                    { "KeyB", "ValueB" }
                }
            };
            // Act
            var result = source.ToLinterModelRelationship(elements);
            // Assert
            MappingAssert.AssertMapped(
                source, result, 
                ignoreDestProps: new[] {
                    nameof(LinterModelRelationship.Source),
                    nameof(LinterModelRelationship.Destination)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrRelationship, object?>>
                {
                    { nameof(LinterModelRelationship.JsonModelId), src => src.ModelId }
                }
            );
            Assert.That(result.Source.Identifier, Is.EqualTo(source.SourceIdentifier));
            Assert.That(result.Destination.Identifier, Is.EqualTo(source.DestinationIdentifier));
        }
    }
}
