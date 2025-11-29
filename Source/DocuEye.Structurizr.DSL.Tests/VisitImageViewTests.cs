using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitImageViewTests : BaseTests
    {
        [Test]
        public void WhenDefineImageViewThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"image systemId viewId 
            {
                title ""Great system title""
                description ""Great system description""
                image /path/to/image
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                
            }");
            var context = parser.imageView();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageView(context);
            // Assert
            Assert.That(result.ElementIdentifier, Is.Not.Null);
            Assert.That(result.ElementIdentifier, Is.EqualTo("systemId"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
            Assert.That(result.Description, Is.EqualTo("Great system description"));
            Assert.That(result.Title, Is.EqualTo("Great system title"));
            Assert.That(result.ImageSource, Is.EqualTo("/path/to/image"));
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Default, Is.False);
        }

        [Test]
        public void WhenDefineImageViewWithWildcardThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"image * viewId 
            {                
            }");
            var context = parser.imageView();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitImageView(context);
            // Assert
            Assert.That(result.ElementIdentifier, Is.Not.Null);
            Assert.That(result.ElementIdentifier, Is.EqualTo("*"));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Identifier, Is.EqualTo("viewId"));
        }
    }
}
