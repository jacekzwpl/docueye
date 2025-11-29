using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitSoftwareSystemTests : BaseTests
    {
        [Test]
        public void WhenDefineSoftwareSystemThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem ""MySystem"" ""Great System"" ""tag1,tag2""
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrSoftwareSystem)visitor.VisitSoftwareSystem(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MySystem"));
            Assert.That(result.Description, Is.EqualTo("Great System"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Software System", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefineSoftwareSystemWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem MySystem ""Great System"" ""tag1,tag2""
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrSoftwareSystem)visitor.VisitSoftwareSystem(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MySystem"));
            Assert.That(result.Description, Is.EqualTo("Great System"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Software System", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
        }

        [Test]
        public void WhenDefineSoftwareSystemWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = softwareSystem MySystem ""Great System"" ""tag1,tag2""
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrSoftwareSystem)visitor.VisitSoftwareSystem(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MySystem"));
            Assert.That(result.Description, Is.EqualTo("Great System"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Software System", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
        }

        [Test]
        public void WhenDefineSoftwareSystemWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = softwareSystem ""MySystem"" ""Great System"" ""tag1,tag2""
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrSoftwareSystem)visitor.VisitSoftwareSystem(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MySystem"));
            Assert.That(result.Description, Is.EqualTo("Great System"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Software System", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
        }

        [Test]
        public void WhenSoftwareSystemNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitSoftwareSystem(context));
        }

        [Test]
        public void WhenDefineSoftwareSystemWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"softwareSystem MySystem {
                description ""Great System""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();
            // Act
            var result = (StructurizrSoftwareSystem)visitor.VisitSoftwareSystem(context);
            // Assert
            Assert.That(result.Name, Is.EqualTo("MySystem"));
            Assert.That(result.Description, Is.EqualTo("Great System"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Software System", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
        }

        [Test]
        public void WhenDefineSoftwareSystemWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"mysystem = softwareSystem MySystem {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitSoftwareSystem(context);
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenDefineSoftwareSystemWithContainerThenContainerExistsInTheModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"mysystem = softwareSystem MySystem {
               container MyContainer
            }
            ");
            var context = parser.softwareSystem();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitSoftwareSystem(context);
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.First(o => o.Type == StructurizrModelElementType.Container).Name, Is.EqualTo("MyContainer"));
            Assert.That(visitor.Workspace.Model.Elements.First(o => o.Type == StructurizrModelElementType.Container).ParentIdentifier, Is.EqualTo("mysystem"));
        }
    }
}
