using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitRelationshipStyleTests : BaseTests
    {
        [Test]
        public void WhenRelationshipStyleIsDefinedThenAllDefinedRPopertiesAreMatched()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            relationship tag1 {
                position 10
                style solid
            } 
            ");
            var context = parser.relationshipStyle();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitRelationshipStyle(context);

            // Assert
            Assert.That(result.Tag, Is.EqualTo("tag1"));
            Assert.That(result.Position, Is.EqualTo(10));
            Assert.That(result.Style, Is.EqualTo("solid"));
        }
    }
}
