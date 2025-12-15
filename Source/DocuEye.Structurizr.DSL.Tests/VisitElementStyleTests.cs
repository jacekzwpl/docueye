using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitElementStyleTests : BaseTests
    {
        [Test]
        public void WhenElementStyleIsDefinedThenAllDefinedRPopertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element tag1 {
                shape box
                border solid
            } 
            ");
            var context = parser.elementStyle();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitElementStyle(context);

            // Assert
            Assert.That(result.Tag, Is.EqualTo("tag1"));
            Assert.That(result.Shape, Is.EqualTo("box"));
            Assert.That(result.Border, Is.EqualTo("solid"));
        }
    }
}
