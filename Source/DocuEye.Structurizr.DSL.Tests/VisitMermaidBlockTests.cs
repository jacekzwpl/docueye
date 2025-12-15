using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitMermaidBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineBlockWithUnixPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mermaid /path/to/file.test
            ");
            var context = parser.mermaidBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenDefineBlockWithWindowsPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mermaid C:\path\to\file.test
            ");
            var context = parser.mermaidBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("C:\\path\\to\\file.test"));
        }

        [Test]
        public void WhenDefineBlockWithUrlThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mermaid https://docueye.com
            ");
            var context = parser.mermaidBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("https://docueye.com"));
        }

        [Test]
        public void WhenDefineBlockWithViewKeyThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mermaid viewKey
            ");
            var context = parser.mermaidBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitMermaidBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("viewKey"));
        }
    }
}
