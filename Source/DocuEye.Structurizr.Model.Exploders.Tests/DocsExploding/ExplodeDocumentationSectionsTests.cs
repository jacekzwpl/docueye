using DocuEye.Structurizr.Json.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Model.Exploders.Tests.DocsExploding
{
    public class ExplodeDocumentationSectionsTests : BaseExploderTests
    {
        [Test]
        public void WhenDocumentationSectionExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var documentationSections = new List<StructurizrJsonDocumentationSection>
            {
                new StructurizrJsonDocumentationSection
                {
                    Content = "content",
                    Format = "markdown",
                    Order = 1,
                }
            };
            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var result = exploder.ExplodeDocumentationSections(documentationSections);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Content, Is.EqualTo("content"));
            Assert.That(result.First().Format, Is.EqualTo("markdown"));
            Assert.That(result.First().Order, Is.EqualTo(1));
        }

        [Test]
        public void WhenMultipleDocumentationSectionsExistsThenAllElementsAreExploded()
        {
            // Arrange
            var documentationSections = new List<StructurizrJsonDocumentationSection>
            {
                new StructurizrJsonDocumentationSection
                {
                    Content = "content",
                    Format = "markdown",
                    Order = 1,
                },
                new StructurizrJsonDocumentationSection
                {
                    Content = "content",
                    Format = "markdown",
                    Order = 1,
                }
            };
            var exploder = new DocumentationExploder(this.mapper);

            // Act
            var result = exploder.ExplodeDocumentationSections(documentationSections);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
