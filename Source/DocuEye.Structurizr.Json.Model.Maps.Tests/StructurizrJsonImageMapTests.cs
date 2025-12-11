using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Docs;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonImageMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonImage_To_ImageToImport_Is_Correct()
        {
            // Arrange
            var image = new StructurizrJsonImage()
            {
                Name = "name",
                Content = "content",
                Type = "type"
            };
            // Act
            var element = image.ConvertToApiModel();
            // Assert
            MappingAssert.AssertMapped(
                image, element,
                ignoreDestProps: new[] { 
                    nameof(ImageToImport.DocumentationId)
                }
            );
        }
    }
}
