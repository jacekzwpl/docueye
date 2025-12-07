using DocuEye.Infrastructure.Tests.Common;
using DocuEye.Structurizr.Json.Model.Maps;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonSystemLandscapeViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonSystemLandscapeView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonSystemLandscapeView
            {
                Key = "system-landscape-view-1",
                Title = "System Landscape View 1",
                EnterpriseBoundaryVisible = true,
                
                PaperSize = "A4_Landscape",
                Dimensions = new StructurizrJsonDimensions
                {
                    Width = 1920,
                    Height = 1080
                },
                Animations = new List<StructurizrJsonAnimationStep>
                {
                    new StructurizrJsonAnimationStep
                    {
                        Order = 1,
                        Elements = new List<string> { "element-1", "element-2" },
                    }
                },
                Description = "A description of the system landscape view.",
                AutomaticLayout = new StructurizrJsonAutomaticLayout
                {
                    Implementation = "implementation",
                    RankDirection = "LeftRight",
                    RankSeparation = 200,
                    NodeSeparation = 100,
                    EdgeSeparation = 20,
                    Vertices = false
                },
                Elements = new List<StructurizrJsonElementView>
                {
                    new StructurizrJsonElementView { Id = "element-1", X = 100, Y = 200 },
                    new StructurizrJsonElementView { Id = "element-2", X = 300, Y = 400 }
                },
                Relationships = new List<StructurizrJsonRelationshipView>
                {
                    new StructurizrJsonRelationshipView { Id = "relationship-1", Routing = "Direct" }
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
                    nameof(ViewToImport.StructurizrElementId),
                    nameof(ViewToImport.AutomaticLayout),
                    nameof(ViewToImport.ContentType),
                    nameof(ViewToImport.BaseViewKey),
                    nameof(ViewToImport.Mode),
                    nameof(ViewToImport.Tags)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonSystemLandscapeView, object?>>
                {
                    { nameof(ViewToImport.ViewType), s => ViewType.SystemLandscapeView },
                    { nameof(ViewToImport.ExternalBoundariesVisible), s => s.EnterpriseBoundaryVisible  }
                }
            );
        }
    }
}
