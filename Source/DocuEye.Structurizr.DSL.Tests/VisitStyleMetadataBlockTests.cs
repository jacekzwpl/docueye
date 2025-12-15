using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleMetadataBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            metadata false
            ");
            var context = parser.styleMetadataBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (bool)visitor.VisitStyleMetadataBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo(false));
        }

        [Test]
        public void WhenBlockIsDefindedWithNotValidValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            metadata notvalid
            ");
            var context = parser.styleMetadataBlock();
            var visitor = new DSLVisitor();


            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitStyleMetadataBlock(context));
        }
    }
}
