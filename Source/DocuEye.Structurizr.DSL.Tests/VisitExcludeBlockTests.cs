using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitExcludeBlockTests : BaseTests
    {
        [Test]
        public void WhenDefineExcludeBlockWithIdentifierThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            exclude mydidentifier
            ");
            var context = parser.excludeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitExcludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "mydidentifier" }));
        }

        [Test]
        public void WhenDefineExcludeBlockWithWildcardThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            exclude *
            ");
            var context = parser.excludeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitExcludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "*" }));
        }

        [Test]
        public void WhenDefineExcludeBlockWithExpresionThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            exclude ""element.tag==test""
            ");
            var context = parser.excludeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitExcludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "element.tag==test" }));
        }

        [Test]
        public void WhenDefineExcludeBlockWithMultipleValuesThenUrlValueIsOk()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            exclude identifier ""element.tag==test""
            ");
            var context = parser.excludeBlock();
            var visitor = new DSLVisitor();
            // Act
            var values = (IEnumerable<string>)visitor.VisitExcludeBlock(context);
            // Assert
            Assert.That(values, Is.EquivalentTo(new[] { "identifier", "element.tag==test" }));
        }
    }
}
