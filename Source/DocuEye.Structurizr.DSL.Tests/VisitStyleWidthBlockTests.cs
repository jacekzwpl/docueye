using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleWidthBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            width 2
            ");
            var context = parser.styleWidthBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStyleWidthBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            width notvalid
            ");
            var context = parser.styleWidthBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleWidthBlock(context));
        }
    }
}
