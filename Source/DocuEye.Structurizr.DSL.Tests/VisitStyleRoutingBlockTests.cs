using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleRoutingBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            routing orthogonal
            ");
            var context = parser.styleRoutingBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleRoutingBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("orthogonal"));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            routing notvalid
            ");
            var context = parser.styleRoutingBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleRoutingBlock(context));
        }
    }
}
