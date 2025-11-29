using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleShapeBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned() {
            // Arrange
            var parser = CreateParserFromText(@"
            shape box
            ");
            var context = parser.styleShapeBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleShapeBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("box"));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            shape notvalid
            ");
            var context = parser.styleShapeBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleShapeBlock(context));
        }
    }
}
