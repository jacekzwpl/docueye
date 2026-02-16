using DocuEye.Infrastructure.Tests.Common;
using DocuEye.Linter.Model;

namespace DocuEye.Structurizr.DSL.Model.Maps.Tests
{
    public class StructurizrModelElementMapTests
    {

        [Test]
        public void Mapping_StructurizrModelElementMap_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrModelElement(
                modelId: "model-1",
                identifier: "element-1"
            )
            {
                ParentIdentifier = "parent-1",
                Name = "Element Name",
                Tags = new List<string> { "Tag1", "Tag2" },
                Technology = "Tech Stack",
                Properties = new Dictionary<string, string>
                {
                    { "Key1", "Value1" },
                    { "Key2", "Value2" }
                },
                Description = "Element Description",
                InstanceOfIdentifier = "instance-1"
            };
            // Act
            var result = source.ToLinterModelElement();
            // Assert
            MappingAssert.AssertMapped(
                source, result, 
                ignoreDestProps: new[] { nameof(LinterModelElement.JsonModelId) }
            );

        }
    }
}
