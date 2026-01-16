using DocuEye.Infrastructure.Tests.Common;
using System;
using System.Collections.Generic;
using System.Text;

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
                SourceIdentifier = "element-1",
                DestinationIdentifier = "element-2",
                Technology = "HTTP",
                Description = "Communicates with",
                Properties = new Dictionary<string, string>
                {
                    { "KeyA", "ValueA" },
                    { "KeyB", "ValueB" }
                }
            };
            // Act
            var result = source.ToLinterModelRelationship(elements);
            // Assert
            MappingAssert.AssertMapped(
                source, result
            );
        }
    }
}
