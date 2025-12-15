using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelGroupBodyTests : BaseTests
    {
        [Test]
        public void WhenPersonIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person Alice
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("Alice"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
        }

        [Test]
        public void WhenMultiplePeopleAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            person Alice
            person Bob
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("Alice"));
            Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("Bob"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
        }
        [Test]
        public void WhenSoftwareSystemIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem MySystem
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MySystem"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
        }

        [Test]
        public void WhenMultipleSoftwareSystemsAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            softwareSystem MySystem
            softwareSystem MySystem2
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MySystem"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("MySystem2"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
        }

        [Test]
        public void WhenInnerGroupIsDefinedTehnItExistsInTheModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group ""My Group""
            {
            }
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Groups.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Groups.First().Name, Is.EqualTo("My Group"));
        }

        [Test]
        public void WhenMultipleInnerGroupsIsDefinedTehnItExistsInTheModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group ""My Group""
            {
            }
            group ""My Group2""
            {
            }
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Groups.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Groups.First().Name, Is.EqualTo("My Group"));
            Assert.That(visitor.Workspace.Model.Groups.Last().Name, Is.EqualTo("My Group2"));
        }

        [Test]
        public void WhenElementIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element MyElement
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyElement"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
        }

        [Test]
        public void WhenMultipleElementsAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            element MyElement1
            element MyElement2
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyElement1"));
            Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("MyElement2"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
        }

        [Test]
        public void WhenElementsWithSameIdentifierAreDefinedTehnExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            elem = element MyElement1
            elem = element MyElement2
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelGroupBody(context, "groupId"));
        }

        [Test]
        public void WhenDeploymentEnvironmentIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentEnvironment Env1
            ");
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
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
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act
            visitor.VisitModelGroupBody(context, "groupId");
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
            var context = parser.modelGroupBody();
            var visitor = new DSLVisitor();
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelGroupBody(context, "groupId"));
        }
    }
}
