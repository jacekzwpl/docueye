using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitThemesBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            themes /path/to/file https://docueye.com
            ");
            var context = parser.themesBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = visitor.VisitThemesBlock(context);

            // Assert
            Assert.That(value.Count(), Is.EqualTo(2));
            Assert.That(value.First(), Is.EqualTo("/path/to/file"));
            Assert.That(value.Last(), Is.EqualTo("https://docueye.com"));
        }
    }
}
