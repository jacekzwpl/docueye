using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitImageBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineBlockWithUnixPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            image /path/to/file.test
            ");
            var context = parser.imageBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitImageBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenDefineBlockWithWindowsPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            image C:\path\to\file.test
            ");
            var context = parser.imageBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitImageBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("C:\\path\\to\\file.test"));
        }

        [Test]
        public void WhenDefineBlockWithUrlThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            image https://docueye.com
            ");
            var context = parser.imageBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitImageBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("https://docueye.com"));
        }

        
    }
}
