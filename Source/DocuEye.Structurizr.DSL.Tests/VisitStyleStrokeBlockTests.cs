using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitStyleStrokeBlockTests : BaseTests
    {
        [Test]
        public void WhenBlockIsDefindedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            stroke #000000
            ");
            var context = parser.styleStrokeBlock();
            var visitor = new DSLVisitor();


            // Act
            var value = (string)visitor.VisitStyleStrokeBlock(context);

            // Assert
            Assert.That(value, Is.EqualTo("#000000"));
        }
    }
}
