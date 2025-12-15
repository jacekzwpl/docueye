using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitComponentTests : BaseTests
    {
        [Test]
        public void WhenDefineContainerThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            component ""MyComponent"" ""Great Component"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitComponent(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyComponent"));
            Assert.That(result.Description, Is.EqualTo("Great Component"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Component", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.ContainerIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineComponentWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            component MyComponent ""Great Component"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitComponent(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyComponent"));
            Assert.That(result.Description, Is.EqualTo("Great Component"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Component", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.ContainerIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineComponentWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = component MyComponent ""Great Component"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitComponent(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyComponent"));
            Assert.That(result.Description, Is.EqualTo("Great Component"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Component", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
            Assert.That(result.ContainerIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineComponentWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = component ""MyComponent"" ""Great Component"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitComponent(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyComponent"));
            Assert.That(result.Description, Is.EqualTo("Great Component"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Component", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
            Assert.That(result.ContainerIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenComponentNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            component
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitComponent(context, "parentId"));
        }

        [Test]
        public void WhenDefineComponentWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"component MyComponent {
                description ""Great Component""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitComponent(context, "parentId");
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyComponent"));
            Assert.That(result.Description, Is.EqualTo("Great Component"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Component", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.ContainerIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineComponentWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"mycomponent = component MyComponent {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitComponent(context, "parentId");
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenDefineComponentWithIdentifierAndModelIdentifiersAreHierarhicalThenIdentifierHasParentIdPart()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mycomponent = component ""MyComponent""
            ");
            var context = parser.component();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical"
            };


            // Act
            var result = visitor.VisitComponent(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyComponent"));
            Assert.That(result.Identifier, Is.EqualTo("parentId.mycomponent"));
            Assert.That(result.ContainerIdentifier, Is.EqualTo("parentId"));
        }
    }
}
