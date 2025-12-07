using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonImageViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonImageView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonImageView
            {
                Key = "image-view-1",
                Title = "Image View 1",
                Description = "A description of the image view.",
                Content = "base64encodedcontent",
                ContentType = "image/png",
                ElementId = "element-123"

            };
            // Act
            var result = source.ToViewToImport();
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(ViewToImport.Elements),
                    nameof(ViewToImport.Relationships),
                    nameof(ViewToImport.AutomaticLayout),
                    nameof(ViewToImport.ViewType),
                    nameof(ViewToImport.PaperSize),
                    nameof(ViewToImport.ExternalBoundariesVisible),
                    nameof(ViewToImport.BaseViewKey),
                    nameof(ViewToImport.Mode),
                    nameof(ViewToImport.Tags)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonImageView, object?>>
                {
                    { nameof(ViewToImport.StructurizrElementId), src => src.ElementId }
                }
            );
        }
    }
}
