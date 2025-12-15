using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitIncludeBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineIncludeBlockWithIdentifierThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            include mydidentifier
            ");
            var context = parser.includeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitIncludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "mydidentifier" }));
        }

        [Test]
        public void WhenDefineIncludeBlockWithWildcardThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            include *
            ");
            var context = parser.includeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitIncludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "*" }));
        }

        [Test]
        public void WhenDefineIncludeBlockWithExpresionThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            include ""element.tag==test""
            ");
            var context = parser.includeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitIncludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "element.tag==test" }));
        }

        [Test]
        public void WhenDefineIncludeBlockWithMultipleValuesThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            include identifier ""element.tag==test""
            ");
            var context = parser.includeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitIncludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "identifier", "element.tag==test" }));
        }
    }
}
