using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitAnimationStepTests : BaseTests
    {
        [Test]
        public void WhenIdentifierIsDefinedTehenExistsInResults() {

            // Arrange
            var parser = CreateParserFromText(@"id1
            ");
            var context = parser.animationStep();
            var visitor = new DSLVisitor();

            // Act
            var result = (IEnumerable<string>)visitor.VisitAnimationStep(context);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First(), Is.EqualTo("id1"));
        }

        [Test]
        public void WhenMutipleIdentifiersAreDefinedThenExistInResults()
        {
            // Arrange
            var parser = CreateParserFromText(@"id1 id2
            ");
            var context = parser.animationStep();
            var visitor = new DSLVisitor();

            // Act
            var result = (IEnumerable<string>)visitor.VisitAnimationStep(context);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First(), Is.EqualTo("id1"));
            Assert.That(result.Last(), Is.EqualTo("id2"));
        }
    }
}
