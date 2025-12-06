using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonFilteredViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonFilteredView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonFilteredView
            {
                Key = "filtered-view-1",
                Title = "Filtered View 1",
                BaseViewKey = "base-view-1",
                Description = "A description of the filtered view.",
                Mode = "SomeMode",
                Tags = new List<string> { "TagA", "TagB" }
                
            };
            // Act
            var result = source.ToViewToImport();
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(ViewToImport.Content),
                    nameof(ViewToImport.Elements),
                    nameof(ViewToImport.Relationships),
                    nameof(ViewToImport.AutomaticLayout),
                    nameof(ViewToImport.ContentType),
                    nameof(ViewToImport.ViewType),
                    nameof(ViewToImport.StructurizrElementId),
                    nameof(ViewToImport.PaperSize),
                    nameof(ViewToImport.ExternalBoundariesVisible)

                }
            );
        }   
    }
}
