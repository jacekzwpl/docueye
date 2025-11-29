using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitImageViewBodyTests : BaseTests
    {
        [Test]
        public void WhenDescriptionIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            description ""Great system description""
            ");
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitImageViewBody(context);

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
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitImageViewBody(context));
        }

        [Test]
        public void WhenTitleIsSetThenCorrectValueIsReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            title ""Great system title""
            ");
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageViewBody(context);
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
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitImageViewBody(context));
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
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageViewBody(context);
            // Assert
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenPlanumlSourceIsSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            plantuml /path/to/file.test
            ");
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageViewBody(context);
            // Assert
            Assert.That(result.PlantUMLSource, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenImageSourceIsSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            image /path/to/file.test
            ");
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageViewBody(context);
            // Assert
            Assert.That(result.ImageSource, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenMermaidSourceIsSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mermaid /path/to/file.test
            ");
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageViewBody(context);
            // Assert
            Assert.That(result.MermaidSource, Is.EqualTo("/path/to/file.test"));
        }

        [Test]
        public void WhenKrokiSourceIsSetThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            kroki plantuml /path/to/file.test
            ");
            var context = parser.imageViewBody();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageViewBody(context);
            // Assert
            Assert.That(result.KrokiConfig?.Format, Is.EqualTo("plantuml"));
            Assert.That(result.KrokiConfig?.Value, Is.EqualTo("/path/to/file.test"));
        }
    }
}
