using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitKrokiBlocksTests : BaseTests
    {
        [Test]
        public void WhenContextIsNullThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitKrokiBlocks(null);
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenContextHasNoElementsThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitKrokiBlocks(new StructurizrDSLParser.KrokiBlockContext[] { });
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenOneKrokiBlockIsDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            kroki plantuml  /path/to/file.test
            ");
            var context = parser.krokiBlock();
            var visitor = new DSLVisitor();
            // Act
            var value = visitor.VisitKrokiBlocks(new[] { context });
            // Assert
            Assert.That(value?.Format, Is.EqualTo("plantuml"));
            Assert.That(value?.Value, Is.EqualTo("/path/to/file.test"));
        }
        [Test]
        public void WhenMultipleKrokiBlocksAreDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser1 = CreateParserFromText(@"
            kroki plantuml mydidentifier1
            ");
            var parser2 = CreateParserFromText(@"
            kroki plantuml mydidentifier2
            ");
            var context1 = parser1.krokiBlock();
            var context2 = parser2.krokiBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitKrokiBlocks(new[] { context1, context2 }));
        }
    }
}
