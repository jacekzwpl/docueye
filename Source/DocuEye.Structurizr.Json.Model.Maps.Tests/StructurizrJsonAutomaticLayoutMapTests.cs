using DocuEye.Infrastructure.Tests.Common;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonAutomaticLayoutMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonAutomaticLayout_To_ViewAutomaticLayout_Works()
        {
            // Arrange
            var source = new StructurizrJsonAutomaticLayout
            {
                Implementation = "implementation",
                RankDirection = "TopBottom",
                RankSeparation = 100,
                NodeSeparation = 50,
                EdgeSeparation = 10,
                Vertices = true
            };
            // Act
            var result = StructurizrJsonAutomaticLayoutMap.ToViewAutomaticLayoutToImport(source);
            // Assert
            MappingAssert.AssertMapped(
                source, result
            );
        }
    }
}
