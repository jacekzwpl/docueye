using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitAnimationBodyTests : BaseTests
    {
        [Test]
        public void WhenStepIsDefinedThenExistsInResults()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            id1 id2
            ");
            var context = parser.animationBody();
            var visitor = new DSLVisitor();
            // Act
            var result = (List<IEnumerable<string>>)visitor.VisitAnimationBody(context);
            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EquivalentTo(new[] { "id1", "id2" }));
        }
        [Test]
        public void WhenMutipleStepsAreDefinedThenExistInResults()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            id1 id2
            id3 id4
            ");
            var context = parser.animationBody();
            var visitor = new DSLVisitor();
            // Act
            var result = (List<IEnumerable<string>>)visitor.VisitAnimationBody(context);
            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First(), Is.EquivalentTo(new[] { "id1", "id2" }));
            Assert.That(result.Last(), Is.EquivalentTo(new[] { "id3", "id4" }));
        }
    }
}
