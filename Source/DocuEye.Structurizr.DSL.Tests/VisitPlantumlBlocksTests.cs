using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitPlantumlBlocksTests : BaseTests
    {
        [Test]
        public void WhenContextIsNullThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlocks(null);
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenContextHasNoElementsThenEmptyListIsReturned()
        {
            // Arrange
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlocks(new StructurizrDSLParser.PlantumlBlockContext[] { });
            // Assert
            Assert.That(values, Is.Null);
        }

        [Test]
        public void WhenOnePlantumlBlockIsDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            plantuml mydidentifier
            ");
            var context = parser.plantumlBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlocks(new[] { context });
            // Assert
            Assert.That(values, Is.EqualTo("mydidentifier"));
        }
        [Test]
        public void WhenMultiplePlantumlBlocksAreDefindedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser1 = CreateParserFromText(@"
            plantuml mydidentifier1
            ");
            var parser2 = CreateParserFromText(@"
            plantuml mydidentifier2
            ");
            var context1 = parser1.plantumlBlock();
            var context2 = parser2.plantumlBlock();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitPlantumlBlocks(new[] { context1, context2 }));
        }
    }
}
