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
    public class StructurizrJsonDynamicViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonDynamicView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonDynamicView
            {
                Key = "dynamic-view-1",
                Title = "Dynamic View 1",
                ElementId = "software-system-1",
                Description = "A description of the dynamic view.",
                PaperSize = "A3_Portrait",
                ExternalBoundariesVisible = true,
                Elements = new List<StructurizrJsonElementView>
                {
                    new StructurizrJsonElementView { Id = "element-1", X = 300, Y = 400 },
                    new StructurizrJsonElementView { Id = "element-2", X = 500, Y = 600 }
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
                    nameof(ViewToImport.ContentType),
                    nameof(ViewToImport.AutomaticLayout),
                    nameof(ViewToImport.BaseViewKey),
                    nameof(ViewToImport.Mode),
                    nameof(ViewToImport.Tags)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonDynamicView, object?>>
                {
                    { nameof(ViewToImport.ViewType), s => ViewType.DynamicView },
                    { nameof(ViewToImport.StructurizrElementId), s => s.ElementId  }
                }
            );
        }
    }
}
