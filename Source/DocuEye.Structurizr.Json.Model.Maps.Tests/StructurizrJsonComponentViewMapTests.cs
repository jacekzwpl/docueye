using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonComponentViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonComponentView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonComponentView
            {
                Key = "component-view-1",
                Title = "Component View 1",
                ContainerId = "container-1",
                PaperSize = "A2_Portrait",
                Description = "A description of the component view.",
                AutomaticLayout = new StructurizrJsonAutomaticLayout
                {
                    Implementation = "implementation",
                    RankDirection = "BottomTop",
                    RankSeparation = 300,
                    NodeSeparation = 150,
                    EdgeSeparation = 30,
                    Vertices = true
                },
                Elements = new List<StructurizrJsonElementView>
                {
                    new StructurizrJsonElementView { Id = "element-1", X = 250, Y = 350 },
                    new StructurizrJsonElementView { Id = "element-2", X = 450, Y = 550 }
                },
                Relationships = new List<StructurizrJsonRelationshipView>
                {
                    new StructurizrJsonRelationshipView { Id = "relationship-1", Routing = "Curved" }
                },
                ExternalContainerBoundariesVisible = false
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
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonComponentView, object?>>
                {
                    { nameof(ViewToImport.ViewType), s => ViewType.ComponentView },
                    { nameof(ViewToImport.StructurizrElementId), s => s.ContainerId },
                    { nameof(ViewToImport.ExternalBoundariesVisible), s => s.ExternalContainerBoundariesVisible }
                });
        }
    }
}
