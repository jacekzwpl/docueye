using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitFilteredViewBodyTests : BaseTests
    {
        [Test]
        public void WhenDescriptionIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description ""Great system description""
            ");
            var context = parser.filteredViewBody();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitFilteredViewBody(context);

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
            var context = parser.filteredViewBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitFilteredViewBody(context));
        }

        [Test]
        public void WhenTitleIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            title ""Great system title""
            ");
            var context = parser.filteredViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitFilteredViewBody(context);
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
            var context = parser.filteredViewBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitFilteredViewBody(context));
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
            var context = parser.filteredViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitFilteredViewBody(context);
            // Assert
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
        }

       

        [Test]
        public void WhenDefaultIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            default

            ");
            var context = parser.filteredViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitFilteredViewBody(context);
            // Assert
            Assert.That(result.Default, Is.EqualTo(true));
        }
    }
}
