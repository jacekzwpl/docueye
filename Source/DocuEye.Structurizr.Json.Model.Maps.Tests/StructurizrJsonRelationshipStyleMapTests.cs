using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonRelationshipStyleMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonRelationshipStyle_To_RelationshipStyleToImport_Is_Correct()
        {
            // Arrange
            var relationshipStyle = new StructurizrJsonRelationshipStyle()
            {
                FontSize = 10,
                Opacity = 5,
                Routing = "Direct",
                Tag = "TestTag",
                Thickness = 3,
                Width = 15,
                Color = "#123456",
                Dashed = true,
                Position = 7
            };
            // Act
            var element = relationshipStyle.ToRelationshipStyleToImport();
            // Assert
            MappingAssert.AssertMapped(
                relationshipStyle, element
            );
        }
    }
}
