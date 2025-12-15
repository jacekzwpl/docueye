using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitTagsBlockTests : BaseTests
    {
        [Test]
        public void WhenTagsBlockHasOneTagThenCorrectValueisReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            tags ""tag1""
            ");
            var context = parser.tagsBlock();
            var visitor = new DSLVisitor();

            // Act
            var result = (List<string>)visitor.VisitTagsBlock(context);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("tag1"));
        }

        [Test]
        public void WhenTagsBlockHasMultipleTagsThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            tags ""tag1"" ""tag2""
            ");
            var context = parser.tagsBlock();
            var visitor = new DSLVisitor();

            // Act
            var result = (List<string>)visitor.VisitTagsBlock(context);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("tag1"));
            Assert.That(result[1], Is.EqualTo("tag2"));
        }
    }
}
