using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleThicknessBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            thickness 12
            ");
            var context = parser.styleThicknessBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStyleThicknessBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(12));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            thickness notvalid
            ");
            var context = parser.styleThicknessBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleThicknessBlock(context));
        }
    }
}
