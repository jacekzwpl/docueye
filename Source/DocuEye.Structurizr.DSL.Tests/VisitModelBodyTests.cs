using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelBodyTests : BaseTests
    {
        [Test]
        public void WhenSetIdentifiersThenIdentifiersHasValidValue()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !identifiers hierarchical
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();

            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);

            // Assert
            Assert.That(model.Identifiers, Is.EqualTo("hierarchical"));
        }

        [Test]
        public void WhenSetInvalidIdentifiersThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !identifiers invalid
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenDuplicateIdentifiersIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            !identifiers flat
            !identifiers flat
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenPersonIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person Alice
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Elements.Count, Is.EqualTo(1));
            Assert.That(model.Elements.First().Name, Is.EqualTo("Alice"));
        }

        [Test]
        public void WhenMultiplePeopleAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person Alice
            person Bob
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Elements.Count, Is.EqualTo(2));
            Assert.That(model.Elements.First().Name, Is.EqualTo("Alice"));
            Assert.That(model.Elements.Last().Name, Is.EqualTo("Bob"));
        }

        [Test]
        public void WhenPeopleWithSameIdentifierAreDefinedTehnExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            alice = person Alice
            alice = person Bob
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenSoftwareSystemIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem MySystem
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Elements.Count, Is.EqualTo(1));
            Assert.That(model.Elements.First().Name, Is.EqualTo("MySystem"));
        }

        [Test]
        public void WhenMultipleSoftwareSystemsAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem MySystem
            softwareSystem MySystem2
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Elements.Count, Is.EqualTo(2));
            Assert.That(model.Elements.First().Name, Is.EqualTo("MySystem"));
            Assert.That(model.Elements.Last().Name, Is.EqualTo("MySystem2"));
        }

        [Test]
        public void WhenSoftwareSystemsWithSameIdentifierAreDefinedTehnExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            system = softwareSystem MySystem
            system = softwareSystem MySystem2
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenRelationshipIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            alice = person Alice
            bob = person Bob
            alice -> bob
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Relationships.Count, Is.EqualTo(1));
            Assert.That(model.Relationships.First().SourceIdentifier, Is.EqualTo("alice"));
            Assert.That(model.Relationships.First().DestinationIdentifier, Is.EqualTo("bob"));
        }

        [Test]
        public void WhenGroupIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group MyGroup {}
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Groups.Count, Is.EqualTo(1));
            Assert.That(model.Groups.First().Name, Is.EqualTo("MyGroup"));
        }

        [Test]
        public void WhenMultipleGroupIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group MyGroup {}
            group MyGroup2 {}
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Groups.Count, Is.EqualTo(2));
            Assert.That(model.Groups.First().Name, Is.EqualTo("MyGroup"));
            Assert.That(model.Groups.Last().Name, Is.EqualTo("MyGroup2"));
        }

        [Test]
        public void WhenSetPropertiesThenPropertiesHasValidValues()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitModelBody(context);

            // Assert
            Assert.That(visitor.Workspace.Model.Properties.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(visitor.Workspace.Model.Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenDuplicatePropertiesSectionIsSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            properties {
                ""key1"" ""value1""
                ""key2"" ""value2""
            }
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenElementIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element MyElement
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Elements.Count, Is.EqualTo(1));
            Assert.That(model.Elements.First().Name, Is.EqualTo("MyElement"));
        }

        [Test]
        public void WhenMultipleElementsAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element MyElement1
            element MyElement2
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(model.Elements.Count, Is.EqualTo(2));
            Assert.That(model.Elements.First().Name, Is.EqualTo("MyElement1"));
            Assert.That(model.Elements.Last().Name, Is.EqualTo("MyElement2"));
        }

        [Test]
        public void WhenElementsWithSameIdentifierAreDefinedTehnExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            elem = element MyElement1
            elem = element MyElement2
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenDeploymentEnvironmentIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentEnvironment Env1
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(visitor.Workspace.Model.DeploymentEnvironments.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.DeploymentEnvironments.First().Name, Is.EqualTo("Env1"));
        }

        [Test]
        public void WhenMultipleDeploymentEnvironmentsAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentEnvironment Env1
            deploymentEnvironment Env2
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act
            var model = (StructurizrModel)visitor.VisitModelBody(context);
            // Assert
            Assert.That(visitor.Workspace.Model.DeploymentEnvironments.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.DeploymentEnvironments.First().Name, Is.EqualTo("Env1"));
            Assert.That(visitor.Workspace.Model.DeploymentEnvironments.Last().Name, Is.EqualTo("Env2"));
        }

        [Test]
        public void WhenDeploymentEnvironmentsWithSameIdentifierAreDefinedTehnExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            elem = deploymentEnvironment Env1
            elem = deploymentEnvironment Env2
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelBody(context));
        }

        [Test]
        public void WhenElementReferenceIsDefindedThenElementIsModified()
        {
            // Arrange
            var workspace = new StructurizrWorkspace();
            workspace.Model.AddModelElement(new StructurizrPerson("elementId", new string[] { "Element" }));
            var parser = CreateParserFromText(@"
            !element elementId {
                tags ""tag1,tag2""
            }
            ");
            var context = parser.modelBody();
            var visitor = new DSLVisitor(workspace);
            // Act
            var result = visitor.VisitModelBody(context);
            // Assert
            var element = visitor.Workspace.Model.Elements.SingleOrDefault(o => o.Identifier == "elementId");
            Assert.That(element, !Is.Null);
            Assert.That(
                element?.Tags,
                Is.EquivalentTo(new[] { "Element", "tag1", "tag2" }));
        }
    }
}
