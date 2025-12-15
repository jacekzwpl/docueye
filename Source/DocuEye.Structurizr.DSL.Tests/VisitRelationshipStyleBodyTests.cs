using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitRelationshipStyleBodyTests : BaseTests
    {
        [Test]
        public void WhenColorBlockIsPresent_ThenColorIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            color ""#ffffff""
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Color, Is.EqualTo("#ffffff"));
        }

        [Test]
        public void WhenFontSizeBlockIsPresent_ThenFontSizeIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            fontSize 12
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.FontSize, Is.EqualTo(12));
        }

        [Test]
        public void WhenWidthBlockIsPresent_ThenWidthIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            width 6
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Width, Is.EqualTo(6));
        }

        [Test]
        public void WhenOpacityBlockIsPresent_ThenOpacityIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            opacity 50
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Opacity, Is.EqualTo(50));
        }

        [Test]
        public void WhenThicknessBlockIsPresent_ThenThicknessIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            thickness 6
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Thickness, Is.EqualTo(6));
        }

        [Test]
        public void WhenStyleBlockIsPresent_ThenStyleIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            style dashed
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Style, Is.EqualTo("dashed"));
        }

        [Test]
        public void WhenRoutingBlockIsPresent_ThenRoutingIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            routing direct
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Routing, Is.EqualTo("direct"));
        }

        [Test]
        public void WhenPositionBlockIsPresent_ThenPositionIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            position 50
            ");
            var visitor = new DSLVisitor();
            var context = parser.relationshipStyleBody();
            // Act
            var result = visitor.VisitRelationshipStyleBody(context);
            // Assert
            Assert.That(result.Position, Is.EqualTo(50));
        }

        [Test]
        public void WhenPropertiesBlockIsPresent_ThenPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
        }
    }
}
