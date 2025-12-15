using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitIncludeBlocksTests : BaseTests
    {
        [Test]
        public void WhenContextIsNullThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitIncludeBlocks(null);
            // Assert
            Assert.That(values, Is.Empty);
        }

        [Test]
        public void WhenContextHasNoElementsThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitIncludeBlocks(new StructurizrDSLParser.IncludeBlockContext[] { });
            // Assert
            Assert.That(values, Is.Empty);
        }

        [Test]
        public void WhenOneIncludeBlockIsDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            include mydidentifier
            ");
            var context = parser.includeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitIncludeBlocks(new[] { context });
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "mydidentifier" }));
        }
        [Test]
        public void WhenMultipleIncludeBlocksAreDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser1 = CreateParserFromText(@"
            include mydidentifier1
            ");
            var parser2 = CreateParserFromText(@"
            include mydidentifier2
            ");
            var context1 = parser1.includeBlock();
            var context2 = parser2.includeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitIncludeBlocks(new[] { context1, context2 });
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "mydidentifier1", "mydidentifier2" }));
        }
    }
}
