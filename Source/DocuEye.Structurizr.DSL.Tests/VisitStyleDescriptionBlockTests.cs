using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleDescriptionBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description false
            ");
            var context = parser.styleDescriptionBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (bool)visitor.VisitStyleDescriptionBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(false));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description notvalid
            ");
            var context = parser.styleDescriptionBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleDescriptionBlock(context));
        }
    }
}
