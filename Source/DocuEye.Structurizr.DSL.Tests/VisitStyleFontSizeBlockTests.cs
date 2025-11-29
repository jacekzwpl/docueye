using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleFontSizeBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            fontSize 12
            ");
            var context = parser.styleFontSizeBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (int)visitor.VisitStyleFontSizeBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(12));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            fontSize notvalid
            ");
            var context = parser.styleFontSizeBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleFontSizeBlock(context));
        }
    }
}
