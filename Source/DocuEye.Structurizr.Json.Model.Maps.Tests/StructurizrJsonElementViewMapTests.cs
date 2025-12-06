using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonElementViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonElementView_To_ElementInViewToImport_Works()
        {
            // Arrange
            var source = new StructurizrJsonElementView
            {
                Id = "element-123",
                X = 150,
                Y = 300
            };
            // Act
            var result = StructurizrJsonElementViewMap.ToElementInViewToImport(source);
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonElementView, object?>>
                {
                    { nameof(ElementInViewToImport.StructurizrId), s => s.Id }
                }
            );
        }
    }
}
