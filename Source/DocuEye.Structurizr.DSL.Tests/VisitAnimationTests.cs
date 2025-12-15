using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitAnimationTests : BaseTests
    {
        [Test]
        public void WhenStepsAreDefinedThenExistsInResults()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            animation {
                id1 id2
                id3 id4
            }
            ");
            var context = parser.animation();
            var visitor = new DSLVisitor();
            // Act
            var result = (List<IEnumerable<string>>)visitor.VisitAnimation(context);
            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First(), Is.EquivalentTo(new[] { "id1", "id2" }));
            Assert.That(result.Last(), Is.EquivalentTo(new[] { "id3", "id4" }));
        }
        
    }
}
