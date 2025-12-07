using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonSystemContextViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonSystemContextView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonSystemContextView
            {
                Key = "system-context-view-1",
                Title = "System Context View 1",
                SoftwareSystemId = "software-system-1",
                EnterpriseBoundaryVisible = true,
                PaperSize = "A4_Portrait",
                Description = "A description of the system context view.",
                AutomaticLayout = new StructurizrJsonAutomaticLayout
                {
                    Implementation = "implementation",
                    RankDirection = "TopBottom",
                    RankSeparation = 150,
                    NodeSeparation = 75,
                    EdgeSeparation = 15,
                    Vertices = true
                },
                Elements = new List<StructurizrJsonElementView>
                {
                    new StructurizrJsonElementView { Id = "element-1", X = 150, Y = 250 },
                    new StructurizrJsonElementView { Id = "element-2", X = 350, Y = 450 }
                },
                Relationships = new List<StructurizrJsonRelationshipView>
                {
                    new StructurizrJsonRelationshipView { Id = "relationship-1", Routing = "Curved" }
                }
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
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonSystemContextView, object?>>
                {
                    { nameof(ViewToImport.ViewType), s => ViewType.SystemContextView },
                    { nameof(ViewToImport.ExternalBoundariesVisible), s => s.EnterpriseBoundaryVisible  },
                    { nameof(ViewToImport.StructurizrElementId), s => s.SoftwareSystemId }
                }


            );
        }
    }
}


