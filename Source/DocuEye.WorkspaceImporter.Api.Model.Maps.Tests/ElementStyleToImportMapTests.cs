using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class ElementStyleToImportMapTests
    {
        [Test]
        public void Mapping_ElementStyleToImport_To_ElementStyle_Works()
        {
            var source = new ElementStyleToImport { 
                Tag = "Software System", 
                Background = "#fff", 
                Color = "#000", 
                Shape = "RoundedBox", 
                Icon = "icon.png",
                Width = 100,
                Height = 100,
                Stroke = "#ccc",
                StrokeWidth = "2",
                FontSize = 12,
                Border = "1px solid #000",
                Opacity = 80,
                Metadata = true,
                Description = true
            };
            var result = source.ToElementStyle();
            MappingAssert.AssertMapped(source, result);
        }
    }
}
