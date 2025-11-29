using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitImageBlocksTests : BaseTests
    {
        [Test]
        public void WhenContextIsNullThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitImageBlocks(null);
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenContextHasNoElementsThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitImageBlocks(new StructurizrDSLParser.ImageBlockContext[] { });
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenOneImageBlockIsDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            image mydidentifier
            ");
            var context = parser.imageBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitImageBlocks(new[] { context });
            // Assert
            Assert.That(values, Is.EqualTo("mydidentifier"));
        }
        [Test]
        public void WhenMultipleImageBlocksAreDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser1 = CreateParserFromText(@"
            image mydidentifier1
            ");
            var parser2 = CreateParserFromText(@"
            image mydidentifier2
            ");
            var context1 = parser1.imageBlock();
            var context2 = parser2.imageBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitImageBlocks(new[] { context1, context2 }));
        }
    }
}
