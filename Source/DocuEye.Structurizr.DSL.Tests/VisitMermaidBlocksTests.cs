using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitMermaidBlocksTests : BaseTests
    {
        [Test]
        public void WhenContextIsNullThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlocks(null);
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenContextHasNoElementsThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlocks(new StructurizrDSLParser.MermaidBlockContext[] { });
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenOneMermaidBlockIsDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mermaid mydidentifier
            ");
            var context = parser.mermaidBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlocks(new[] { context });
            // Assert
            Assert.That(values, Is.EqualTo("mydidentifier"));
        }
        [Test]
        public void WhenMultipleMermaidBlocksAreDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser1 = CreateParserFromText(@"
            mermaid mydidentifier1
            ");
            var parser2 = CreateParserFromText(@"
            mermaid mydidentifier2
            ");
            var context1 = parser1.mermaidBlock();
            var context2 = parser2.mermaidBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitMermaidBlocks(new[] { context1, context2 }));
        }
    }
}
