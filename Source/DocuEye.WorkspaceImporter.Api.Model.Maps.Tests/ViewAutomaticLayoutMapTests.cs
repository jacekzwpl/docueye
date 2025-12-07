using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using DocuEye.WorkspaceImporter.Api.Model.Maps;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class ViewAutomaticLayoutMapTests
    {
        [Test]
        public void Mapping_ViewAutomaticLayout_To_AutomaticLayout_Works()
        {
            // Arrange
            var source = new ViewAutomaticLayout
            {
                Implementation = "implementation",
                RankDirection = "TopBottom",
                RankSeparation = 100,
                NodeSeparation = 50,
                EdgeSeparation = 10,
                Vertices = true
            };
            // Act
            var result = source.ToAutomaticLayout();
            // Assert
            MappingAssert.AssertMapped(source, result);
        }
    }
}
