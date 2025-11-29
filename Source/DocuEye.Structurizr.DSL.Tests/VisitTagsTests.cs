using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitTagsTests : BaseTests
    {
        [Test]
        public void WhenOneTagIsDefinedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"""tag1""");
            var context = parser.tags();
            var visitor = new DSLVisitor();

            // Act
            var result = (string[])visitor.VisitTags(context);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0], Is.EqualTo("tag1"));
        }

        [Test]
        public void WhenMultipleTagsAreDefinedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"""tag1,tag2""");
            var context = parser.tags();
            var visitor = new DSLVisitor();

            // Act
            var result = (string[])visitor.VisitTags(context);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result[0], Is.EqualTo("tag1"));
            Assert.That(result[1], Is.EqualTo("tag2"));
        }
    }
}
