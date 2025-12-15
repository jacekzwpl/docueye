using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleColorBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            color #ff0000
            ");
            var context = parser.styleColorBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleColorBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("#ff0000"));
        }
    }
}
