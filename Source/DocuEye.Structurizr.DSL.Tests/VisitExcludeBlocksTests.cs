using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitExcludeBlocksTests : BaseTests
    {
        [Test]
        public void WhenContextIsNullThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitExcludeBlocks(null);
            // Assert
            Assert.That(values, Is.Empty);
        }
        [Test]
        public void WhenContextHasNoElementsThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitExcludeBlocks(new StructurizrDSLParser.ExcludeBlockContext[] { });
            // Assert
            Assert.That(values, Is.Empty);
        }
        [Test]
        public void WhenOneExcludeBlockIsDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            exclude mydidentifier
            ");
            var context = parser.excludeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitExcludeBlocks(new[] { context });
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "mydidentifier" }));
        }
        [Test]
        public void WhenMultipleExcludeBlocksAreDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser1 = CreateParserFromText(@"
            exclude mydidentifier1
            ");
            var parser2 = CreateParserFromText(@"
            exclude mydidentifier2
            ");
            var context1 = parser1.excludeBlock();
            var context2 = parser2.excludeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitExcludeBlocks(new[] { context1, context2 });
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "mydidentifier1", "mydidentifier2" }));
        }
    }
}
