using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStylePositionBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            position 12
            ");
            var context = parser.stylePositionBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStylePositionBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(12));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            position notvalid
            ");
            var context = parser.stylePositionBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStylePositionBlock(context));
        }
    }
}
