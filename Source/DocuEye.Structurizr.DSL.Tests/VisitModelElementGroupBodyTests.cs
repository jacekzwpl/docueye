using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitModelElementGroupBodyTests : BaseTests
    {
        [Test]
        public void WhenContainerIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            container MyContainer
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var system = new StructurizrSoftwareSystem("system");
            // Act
            visitor.VisitModelElementGroupBody(context, system.ToModelElement(), "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyContainer"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.First().ParentIdentifier, Is.EqualTo("system"));
        }

        [Test]
        public void WhenMultipleContainersAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            container MyContainer
            container MyContainer2
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var system = new StructurizrSoftwareSystem("system");
            // Act
            visitor.VisitModelElementGroupBody(context, system.ToModelElement(), "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyContainer"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("MyContainer2"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.First().ParentIdentifier, Is.EqualTo("system"));
            Assert.That(visitor.Workspace.Model.Elements.Last().ParentIdentifier, Is.EqualTo("system"));
        }

        [Test]
        public void WhenComponentIsDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            component MyComponent
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var container = new StructurizrContainer("container", "system");
            // Act
            visitor.VisitModelElementGroupBody(context, container.ToModelElement(), "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyComponent"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.First().ParentIdentifier, Is.EqualTo("container"));
        }

        [Test]
        public void WhenMultipleComponentsAreDefindedThenExistsInModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            component MyComponent
            component MyComponent2
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var container = new StructurizrContainer("container", "system");
            // Act
            visitor.VisitModelElementGroupBody(context, container.ToModelElement(), "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyComponent"));
            Assert.That(visitor.Workspace.Model.Elements.First().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.Last().Name, Is.EqualTo("MyComponent2"));
            Assert.That(visitor.Workspace.Model.Elements.Last().GroupId, Is.EqualTo("groupId"));
            Assert.That(visitor.Workspace.Model.Elements.First().ParentIdentifier, Is.EqualTo("container"));
            Assert.That(visitor.Workspace.Model.Elements.Last().ParentIdentifier, Is.EqualTo("container"));
        }

        [Test]
        public void WhenComponentIsDefindedInSoftwareSystemThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            component MyContainer
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var system = new StructurizrSoftwareSystem("system");
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelElementGroupBody(context, system.ToModelElement(), "groupId"));
        }

        [Test]
        public void WhenContainerIsDefindedInContainerThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            container MyContainer
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var container = new StructurizrContainer("container", "system");
            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitModelElementGroupBody(context, container.ToModelElement(), "groupId"));
        }


        [Test]
        public void WhenInnerGroupIsDefinedThenItExistsInTheModel()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            group ""My Group""
            {
            }
            ");
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var system = new StructurizrSoftwareSystem("system");
            // Act
            visitor.VisitModelElementGroupBody(context, system.ToModelElement(), "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Groups.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Groups.First().Name, Is.EqualTo("My Group"));
        }

        [Test]
        public void WhenMultipleInnerGroupsIsDefinedThenItExistsInTheModel()
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
            var context = parser.modelElementGroupBody();
            var visitor = new DSLVisitor();
            var system = new StructurizrSoftwareSystem("system");
            // Act
            visitor.VisitModelElementGroupBody(context, system.ToModelElement(), "groupId");
            // Assert
            Assert.That(visitor.Workspace.Model.Groups.Count, Is.EqualTo(2));
            Assert.That(visitor.Workspace.Model.Groups.First().Name, Is.EqualTo("My Group"));
            Assert.That(visitor.Workspace.Model.Groups.Last().Name, Is.EqualTo("My Group2"));
        }
    }
}
