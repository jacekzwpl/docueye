using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static StructurizrDSLParser;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitElementStyleBodyTests : BaseTests
    {
        [Test]
        public void WhenShapeBlockIsPresent_ThenShapeIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            shape box
            ");

            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Shape, Is.EqualTo("box"));
        }

        [Test]
        public void WhenIconBlockIsPresent_ThenIconIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            icon ""icon""
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Icon, Is.EqualTo("icon"));
        }

        [Test]
        public void WhenWidthBlockIsPresent_ThenWidthIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            width 100
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Width, Is.EqualTo(100));
        }
        [Test]
        public void WhenHeightBlockIsPresent_ThenHeightIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            height 100
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Height, Is.EqualTo(100));
        }
        [Test]
        public void WhenBackgroundBlockIsPresent_ThenBackgroundIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            background ""#ffffff""
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Background, Is.EqualTo("#ffffff"));
        }
        [Test]
        public void WhenColorBlockIsPresent_ThenColorIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            color ""#ffffff""
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Color, Is.EqualTo("#ffffff"));
        }
        [Test]
        public void WhenStrokeBlockIsPresent_ThenStrokeIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            stroke ""#ffffff""
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Stroke, Is.EqualTo("#ffffff"));
        }
        [Test]
        public void WhenStrokeWidthBlockIsPresent_ThenStrokeWidthIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            strokeWidth 6
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.StrokeWidth, Is.EqualTo(6));
        }
        [Test]
        public void WhenFontSizeBlockIsPresent_ThenFontSizeIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            fontSize 100
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.FontSize, Is.EqualTo(100));
        }
        [Test]
        public void WhenBorderBlockIsPresent_ThenBorderIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            border solid
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Border, Is.EqualTo("solid"));
        }
        [Test]
        public void WhenOpacityBlockIsPresent_ThenOpacityIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            opacity 100
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Opacity, Is.EqualTo(100));
        }
        [Test]
        public void WhenMetadataBlockIsPresent_ThenMetadataIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            metadata true
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Metadata, Is.True);
        }
        [Test]
        public void WhenDescriptionBlockIsPresent_ThenDescriptionIsSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description true
            ");
            var visitor = new DSLVisitor();
            var context = parser.elementStyleBody();
            // Act
            var result = visitor.VisitElementStyleBody(context);
            // Assert
            Assert.That(result.Description, Is.True);
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
