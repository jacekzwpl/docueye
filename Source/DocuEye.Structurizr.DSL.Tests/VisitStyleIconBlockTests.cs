using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleIconBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            icon /path/to/icon
            ");
            var context = parser.styleIconBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleIconBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("/path/to/icon"));
        }
    }
}
