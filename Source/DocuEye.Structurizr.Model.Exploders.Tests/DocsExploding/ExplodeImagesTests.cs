using DocuEye.Structurizr.Json.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Model.Exploders.Tests.DocsExploding
{
    public class ExplodeImagesTests : BaseExploderTests
    {
        [Test]
        public void WhenImageExsistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var images = new List<StructurizrJsonImage>
            {
                new StructurizrJsonImage
                {
                    Content = "base64",
                    Name = "image",
                    Type = "png",
                }
            };

            var exploder = new DocumentationExploder();
            // Act
            var result = exploder.ExplodeImages(images, "documentationId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Content, Is.EqualTo("base64"));
            Assert.That(result.First().Name, Is.EqualTo("image"));
            Assert.That(result.First().Type, Is.EqualTo("png"));
            Assert.That(result.First().DocumentationId, Is.EqualTo("documentationId"));
            
        }

        [Test]
        public void WhenMultipleImagesExistsThenAllElementsAreExploded()
        {
            // Arrange
            var images = new List<StructurizrJsonImage>
            {
                new StructurizrJsonImage
                {
                    Content = "base64",
                    Name = "image",
                    Type = "png",
                },
                new StructurizrJsonImage
                {
                    Content = "base64",
                    Name = "image",
                    Type = "png",
                }
            };

            var exploder = new DocumentationExploder();
            // Act
            var result = exploder.ExplodeImages(images, "documentationId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
