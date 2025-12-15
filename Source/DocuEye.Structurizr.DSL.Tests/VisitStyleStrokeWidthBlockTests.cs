using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleStrokeWidthBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            strokewidth 6
            ");
            var context = parser.styleStrokeWidthBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStyleStrokeWidthBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(6));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            strokewidth notvalid
            ");
            var context = parser.styleStrokeWidthBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleStrokeWidthBlock(context));
        }
    }
}
