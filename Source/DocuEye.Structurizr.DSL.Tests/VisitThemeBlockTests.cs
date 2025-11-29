using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitThemeBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            theme /path/to/file
            ");
            var context = parser.themeBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitThemeBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("/path/to/file"));
        }
    }
}
