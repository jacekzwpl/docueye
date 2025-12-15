using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitContainerTests : BaseTests
    {
        [Test]
        public void WhenDefineContainerThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            container ""MyContainer"" ""Great Container"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitContainer(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyContainer"));
            Assert.That(result.Description, Is.EqualTo("Great Container"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.SystemIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineContainerWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            container MyContainer ""Great Container"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitContainer(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyContainer"));
            Assert.That(result.Description, Is.EqualTo("Great Container"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.SystemIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineContainerWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = container MyContainer ""Great Container"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitContainer(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyContainer"));
            Assert.That(result.Description, Is.EqualTo("Great Container"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
            Assert.That(result.SystemIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineContainerWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = container ""MyContainer"" ""Great Container"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitContainer(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyContainer"));
            Assert.That(result.Description, Is.EqualTo("Great Container"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
            Assert.That(result.SystemIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenContainerNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            container
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitContainer(context, "parentId"));
        }

        [Test]
        public void WhenDefineContainerWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"container MyContainer {
                description ""Great Container""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitContainer(context, "parentId");
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyContainer"));
            Assert.That(result.Description, Is.EqualTo("Great Container"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.SystemIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineContainerWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"mycontainer = container MyContainer {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitContainer(context,"parentId");
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenDefineContainerWithIdentifierAndModelIdentifiersAreHierarhicalThenIdentifierHasParentIdPart()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mycontainer = container ""MyContainer""
            ");
            var context = parser.container();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical"
            };


            // Act
            var result = visitor.VisitContainer(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyContainer"));
            Assert.That(result.Identifier, Is.EqualTo("parentId.mycontainer"));
            Assert.That(result.SystemIdentifier, Is.EqualTo("parentId"));
        }
    }
}
