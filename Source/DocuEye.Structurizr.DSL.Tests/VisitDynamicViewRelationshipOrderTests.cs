using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDynamicViewRelationshipOrderTests : BaseTests
    {
        [Test]
        public void WhenValidIntegerIsDefinedThenValidResultIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            1:
            ");
            var context = parser.dynamicViewRelationshipOrder();
            var visitor = new DSLVisitor();

            // Act
            var result = (int)visitor.VisitDynamicViewRelationshipOrder(context);

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }
        [Test]
        public void WhenNotValidIntegerIsDefinedThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            aaa1:
            ");
            var context = parser.dynamicViewRelationshipOrder();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitDynamicViewRelationshipOrder(context));
        }

        [Test]
        public void WhenNegativeIntegerIsDefinedThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            -2:
            ");
            var context = parser.dynamicViewRelationshipOrder();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitDynamicViewRelationshipOrder(context));
        }

    }
}
