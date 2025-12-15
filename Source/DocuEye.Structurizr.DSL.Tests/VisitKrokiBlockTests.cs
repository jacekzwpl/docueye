using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitKrokiBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineBlockWithUnixPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            kroki plantuml /path/to/file.test
            ");
            var context = parser.krokiBlock();
            var visitor = new DSLVisitor();
            // Act
            var value = visitor.VisitKrokiBlock(context);
            // Assert
            Assert.That(value.Format, Is.EqualTo("plantuml"));
            Assert.That(value.Value, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenDefineBlockWithWindowsPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            kroki plantuml C:\path\to\file.test
            ");
            var context = parser.krokiBlock();
            var visitor = new DSLVisitor();
            // Act
            var value = visitor.VisitKrokiBlock(context);
            // Assert
            Assert.That(value.Format, Is.EqualTo("plantuml"));
            Assert.That(value.Value, Is.EqualTo("C:\\path\\to\\file.test"));
        }

        [Test]
        public void WhenDefineBlockWithUrlThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            kroki plantuml https://docueye.com
            ");
            var context = parser.krokiBlock();
            var visitor = new DSLVisitor();
            // Act
            var value = visitor.VisitKrokiBlock(context);
            // Assert
            Assert.That(value.Format, Is.EqualTo("plantuml"));
            Assert.That(value.Value, Is.EqualTo("https://docueye.com"));
        }

        
    }
}
