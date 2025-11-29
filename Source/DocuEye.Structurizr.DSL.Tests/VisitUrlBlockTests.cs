using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitUrlBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineUrlBlockThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            url https://docueye.com
            ");
            var context = parser.urlBlock();
            var visitor = new DSLVisitor();
            // Act
            var url = (string)visitor.VisitUrlBlock(context);
            // Assert
            Assert.That(url, Is.EqualTo("https://docueye.com"));
        }

        [Test]
        public void WhenDefineInvalidUrlValueThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            url docueye.com
            ");
            var context = parser.urlBlock();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitUrlBlock(context));
        }
    }
}
