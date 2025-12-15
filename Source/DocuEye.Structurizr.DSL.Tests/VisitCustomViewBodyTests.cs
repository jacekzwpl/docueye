using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitCustomViewBodyTests : BaseTests
    {
        [Test]
        public void WhenDescriptionIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description ""Great system description""
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitCustomViewBody(context);

            // Assert
            Assert.That(result.Description, Is.EqualTo("Great system description"));
        }

        [Test]
        public void WhenDuplicateDescriptionIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description ""Great system description""
            description ""Great system description""
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitCustomViewBody(context));
        }

        [Test]
        public void WhenTitleIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            title ""Great system title""
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.Title, Is.EqualTo("Great system title"));
        }
        [Test]
        public void WhenDuplicateTitleIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            title ""Great system title""
            title ""Great system title""
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitCustomViewBody(context));
        }

        [Test]
        public void WhenIncludeBlocksAreSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            include mydidentifier1
            include mydidentifier2
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.Include, Is.EquivalentTo(new[] { "mydidentifier1", "mydidentifier2" }));
        }

        [Test]
        public void WhenExcludeBlocksAreSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            exclude mydidentifier1
            exclude mydidentifier2
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.Exclude, Is.EquivalentTo(new[] { "mydidentifier1", "mydidentifier2" }));
        }

        [Test]
        public void WhenPropertiesAreSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties
            {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenAnimationIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            animation
            {
                key1 key2
                key3 key4
            }
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.Animation?.Count(), Is.EqualTo(2));
            Assert.That(result.Animation.First(), Is.EquivalentTo(new string[] { "key1", "key2" }));
            Assert.That(result.Animation.Last(), Is.EquivalentTo(new string[] { "key3", "key4" }));
        }

        [Test]
        public void WhenDefaultIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            default

            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.Default, Is.EqualTo(true));
        }

        [Test]
        public void WhenAutolayoutIsDefinedThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            autolayout lr 100 200
            ");
            var context = parser.customViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitCustomViewBody(context);
            // Assert
            Assert.That(result.AutomaticLayout?.RankDirection, Is.EqualTo("lr"));
            Assert.That(result.AutomaticLayout?.RankSeparation, Is.EqualTo(100));
            Assert.That(result.AutomaticLayout?.NodeSeparation, Is.EqualTo(200));
        }
    }
}
