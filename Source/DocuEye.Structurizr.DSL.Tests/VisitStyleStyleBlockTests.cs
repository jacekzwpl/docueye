using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleStyleBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            style solid
            ");
            var context = parser.styleStyleBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleStyleBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("solid"));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            style notvalid
            ");
            var context = parser.styleStyleBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleStyleBlock(context));
        }
    }
}
