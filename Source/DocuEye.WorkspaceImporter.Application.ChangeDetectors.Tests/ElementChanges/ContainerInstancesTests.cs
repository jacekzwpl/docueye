using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class ContainerInstancesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenContainerInstanceDoNotExistsThenElementWillBeAdded()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "4",
                    StructurizrInstanceOfId = "2",
                    StructurizrParentId = "3",
                    DslId = "containerinstance1",
                    Type = ElementType.ContainerInstance
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "ID1",
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new Element
                {
                    Id = "ID2",
                    StructurizrId = "2",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new Element
                {
                    Id = "ID3",
                    StructurizrId = "3",
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },

            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectContainerInstancesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(4));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("4"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("containerinstance1"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.ContainerInstance));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements[2].Id));
            Assert.That(existingElements.Last().InstanceOfId, Is.EqualTo(existingElements[1].Id));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Container 1"));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));

        }

        [Test]
        public async Task WhenContainerInstanceExistsThenElementWillBeUpdated()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "4",
                    StructurizrInstanceOfId = "2",
                    StructurizrParentId = "3",
                    DslId = "containerinstance1",
                    Type = ElementType.ContainerInstance
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "ID1",
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new Element
                {
                    Id = "ID2",
                    StructurizrId = "2",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new Element
                {
                    Id = "ID3",
                    StructurizrId = "3",
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },
                new Element
                {
                    Id = "ID4",
                    StructurizrId = "4",
                    InstanceOfId = "ID2",
                    ParentId = "ID3",
                    DslId = "containerinstance1",
                    Type = ElementType.ContainerInstance
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectContainerInstancesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(4));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("4"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("containerinstance1"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.ContainerInstance));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements[2].Id));
            Assert.That(existingElements.Last().InstanceOfId, Is.EqualTo(existingElements[1].Id));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Container 1"));
            Assert.That(existingElements.Last().Id, Is.EqualTo("ID4"));
        }

        [Test]
        public async Task WhenContainerInstanceExistsButIsNotInImportThenElementWillBeDeleted()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "ID1",
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new Element
                {
                    Id = "ID2",
                    StructurizrId = "2",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new Element
                {
                    Id = "ID3",
                    StructurizrId = "3",
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },
                new Element
                {
                    Id = "ID4",
                    StructurizrId = "4",
                    InstanceOfId = "ID2",
                    ParentId = "ID3",
                    DslId = "containerinstance1",
                    Type = ElementType.ContainerInstance
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectContainerInstancesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(3));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("ID4"));
        }
    }
}
