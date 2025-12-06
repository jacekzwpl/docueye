using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonContainerViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonContainerView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonContainerView
            {
                Key = "container-view-1",
                Title = "Container View 1",
                SoftwareSystemId = "software-system-1",
                PaperSize = "A3_Landscape",
                Description = "A description of the container view.",
                AutomaticLayout = new StructurizrJsonAutomaticLayout
                {
                    Implementation = "implementation",
                    RankDirection = "RightLeft",
                    RankSeparation = 250,
                    NodeSeparation = 125,
                    EdgeSeparation = 25,
                    Vertices = false
                },
                Elements = new List<StructurizrJsonElementView>
                {
                    new StructurizrJsonElementView { Id = "element-1", X = 200, Y = 300 },
                    new StructurizrJsonElementView { Id = "element-2", X = 400, Y = 500 }
                },
                Relationships = new List<StructurizrJsonRelationshipView>
                {
                    new StructurizrJsonRelationshipView { Id = "relationship-1", Routing = "Orthogonal" }
                },
                ExternalSoftwareSystemBoundariesVisible = true,
                
            };
            // Act
            var result = source.ToViewToImport();
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(ViewToImport.Content),
                    nameof(ViewToImport.Relationships),
                    nameof(ViewToImport.Elements),
                    nameof(ViewToImport.AutomaticLayout),
                    nameof(ViewToImport.ContentType),
                    nameof(ViewToImport.BaseViewKey),
                    nameof(ViewToImport.Mode),
                    nameof(ViewToImport.Tags)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonContainerView, object?>>
                {
                    { nameof(ViewToImport.ViewType), s => ViewType.ContainerView },
                    { nameof(ViewToImport.StructurizrElementId), s => s.SoftwareSystemId },
                    { nameof(ViewToImport.ExternalBoundariesVisible), s => s.ExternalSoftwareSystemBoundariesVisible }
                }
            );
        }
    }
}
