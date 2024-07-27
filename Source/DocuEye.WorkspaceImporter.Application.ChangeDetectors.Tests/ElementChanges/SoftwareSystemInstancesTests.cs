using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class SoftwareSystemInstancesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenSoftwareSystemInstanceDoNotExistsThenElementWillBeAdded()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new ElementToImport
                {
                    StructurizrId = "2",
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },
                new ElementToImport
                {
                    StructurizrId = "3",
                    StructurizrInstanceOfId = "1",
                    StructurizrParentId = "2",
                    DslId = "softwaresysteminstance1",
                    Type = ElementType.SoftwareSystemInstance
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
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectSoftwareSystemInstancesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(3));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("3"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("softwaresysteminstance1"));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Software System 1"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.SoftwareSystemInstance));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements[1].Id));
            Assert.That(existingElements.Last().InstanceOfId, Is.EqualTo(existingElements[0].Id));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task WhenSoftwareSystemInstanceExistsThenElementWillBeUpdated()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new ElementToImport
                {
                    StructurizrId = "2",
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },
                new ElementToImport
                {
                    StructurizrId = "3",
                    StructurizrInstanceOfId = "1",
                    StructurizrParentId = "2",
                    DslId = "softwaresysteminstance1",
                    Type = ElementType.SoftwareSystemInstance
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
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },
                new Element
                {
                    Id = "ID3",
                    StructurizrId = "3",
                    ParentId = "ID2",
                    InstanceOfId = "ID1",
                    DslId = "softwaresysteminstance1",
                    Type = ElementType.SoftwareSystemInstance,
                    Name = "Software System 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectSoftwareSystemInstancesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(3));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("3"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("softwaresysteminstance1"));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Software System 1"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.SoftwareSystemInstance));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements[1].Id));
            Assert.That(existingElements.Last().InstanceOfId, Is.EqualTo(existingElements[0].Id));

        }

        [Test]
        public async Task WhenSoftwareSystemInstanceExistsButIsNotInImportThenElementWillBeDeleted()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new ElementToImport
                {
                    StructurizrId = "2",
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
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
                    DslId = "deploymentNode1",
                    Type = ElementType.DeploymentNode,
                    Name = "Deployment Node 1"
                },
                new Element
                {
                    Id = "ID3",
                    StructurizrId = "3",
                    ParentId = "ID2",
                    InstanceOfId = "ID1",
                    DslId = "softwaresysteminstance1",
                    Type = ElementType.SoftwareSystemInstance,
                    Name = "Software System 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectSoftwareSystemInstancesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(2));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("ID3"));
        }
    }
}
