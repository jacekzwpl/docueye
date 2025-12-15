using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitPlantumlBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineBlockWithUnixPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            plantuml /path/to/file.test
            ");
            var context = parser.plantumlBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenDefineBlockWithWindowsPathThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            plantuml C:\path\to\file.test
            ");
            var context = parser.plantumlBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("C:\\path\\to\\file.test"));
        }

        [Test]
        public void WhenDefineBlockWithUrlThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            plantuml https://docueye.com
            ");
            var context = parser.plantumlBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("https://docueye.com"));
        }

        [Test]
        public void WhenDefineBlockWithViewKeyThenRerutnValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            plantuml viewKey
            ");
            var context = parser.plantumlBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = visitor.VisitPlantumlBlock(context);
            // Assert
            Assert.That(values, Is.EqualTo("viewKey"));
        }
    }
}
