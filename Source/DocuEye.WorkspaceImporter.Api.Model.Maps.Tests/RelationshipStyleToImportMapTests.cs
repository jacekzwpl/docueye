using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class RelationshipStyleToImportMapTests
    {
        [Test]
        public void Mapping_RelationshipStyleToImport_To_RelationshipStyle_Works()
        {
            var source = new RelationshipStyleToImport { 
                Tag = "Relationship", 
                Color = "#333", 
                Routing = "Orthogonal", 
                Dashed = true,
                FontSize = 12,
                Opacity = 80,
                Position = 1,
                Thickness = 2,
                Width = 100
            };
            var result = source.ToRelationshipStyle();
            MappingAssert.AssertMapped(source, result);
        }
    }
}
