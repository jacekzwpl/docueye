using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleOpacityBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            opacity 50
            ");
            var context = parser.styleOpacityBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStyleOpacityBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(50));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            opacity notvalid
            ");
            var context = parser.styleOpacityBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleOpacityBlock(context));
        }
    }
}
