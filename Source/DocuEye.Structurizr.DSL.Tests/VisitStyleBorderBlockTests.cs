using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleBorderBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            border solid
            ");
            var context = parser.styleBorderBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleBorderBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("solid"));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            border notvalid
            ");
            var context = parser.styleBorderBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleBorderBlock(context));
        }
    }
}
