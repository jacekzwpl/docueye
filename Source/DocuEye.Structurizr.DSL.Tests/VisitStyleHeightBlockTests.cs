using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleHeightBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            height 2
            ");
            var context = parser.styleHeightBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStyleHeightBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(2));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            height notvalid
            ");
            var context = parser.styleHeightBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleHeightBlock(context));
        }
    }
}
