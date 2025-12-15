using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitElementTests : BaseTests
    {
        [Test]
        public void WhenDefineElementThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element ""MyElement"" ""Element Type"" ""Great Element"" ""tag1,tag2""
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrCustomElement)visitor.VisitElement(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyElement"));
            Assert.That(result.Description, Is.EqualTo("Great Element"));
            Assert.That(result.Metadata, Is.EqualTo("Element Type"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefineElementWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element MyElement ""Element Type"" ""Great Element"" ""tag1,tag2""
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrCustomElement)visitor.VisitElement(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyElement"));
            Assert.That(result.Description, Is.EqualTo("Great Element"));
            Assert.That(result.Metadata, Is.EqualTo("Element Type"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefineElementWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = element MyElement ""Element Type"" ""Great Element"" ""tag1,tag2""
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrCustomElement)visitor.VisitElement(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyElement"));
            Assert.That(result.Description, Is.EqualTo("Great Element"));
            Assert.That(result.Metadata, Is.EqualTo("Element Type"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
        }

        [Test]
        public void WhenDefineElementWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = element ""MyElement"" ""Element Type"" ""Great Element"" ""tag1,tag2""
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrCustomElement)visitor.VisitElement(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyElement"));
            Assert.That(result.Description, Is.EqualTo("Great Element"));
            Assert.That(result.Metadata, Is.EqualTo("Element Type"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
        }

        [Test]
        public void WhenElementNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitElement(context));
        }

        [Test]
        public void WhenDefineElementWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"element MyElement {
                description ""Great Element""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();
            // Act
            var result = (StructurizrCustomElement)visitor.VisitElement(context);
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyElement"));
            Assert.That(result.Description, Is.EqualTo("Great Element"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
        }

        [Test]
        public void WhenDefineElementWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"myelement = element MyElement {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.element();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitElement(context);
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }
    }
}
