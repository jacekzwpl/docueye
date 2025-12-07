using DocuEye.Infrastructure.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonElementStyleMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonElementStyle_To_ElementStyleToImport_Is_Correct()
        {
            // Arrange
            var elementStyle = new StructurizrJsonElementStyle()
            {
                Background = "#FFFFFF",
                Border = "#000000",
                Color = "#FF0000",
                Description = true,
                FontSize = 12,
                Height = 100,
                Icon = "icon.png",
                Metadata = true,
                Opacity = 8,
                Shape = "RoundedBox",
                Stroke = "#00FF00",
                StrokeWidth = "2",
                Tag = "TestTag",
                Width = 200
            };
            // Act
            var element = elementStyle.ToElementStyleToImport();
            // Assert
            MappingAssert.AssertMapped(
                elementStyle, element
            );
        }
    }
}
