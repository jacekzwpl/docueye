using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class ComponentsTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenComponentDoNotExistsThenElementWillBeAdded()
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
                    StructurizrParentId = "1",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new ElementToImport
                {
                    StructurizrId = "3",
                    StructurizrParentId = "2",
                    DslId = "component1",
                    Type = ElementType.Component,
                    Name = "Component 1"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new Element
                {
                    Id = "testid2",
                    StructurizrId = "2",
                    ParentId = "testid1",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectComponentsChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(3));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("3"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("component1"));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Component 1"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.Component));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements[1].Id));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task WhenComponentExistsThenElementWillBeUpdated()
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
                    StructurizrParentId = "1",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new ElementToImport
                {
                    StructurizrId = "3",
                    StructurizrParentId = "2",
                    DslId = "component1",
                    Type = ElementType.Component,
                    Name = "Component 1 changed"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new Element
                {
                    Id = "testid2",
                    StructurizrId = "2",
                    ParentId = "testid1",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new Element
                {
                    Id = "testid3",
                    StructurizrId = "3",
                    ParentId = "testid2",
                    DslId = "component1",
                    Type = ElementType.Component,
                    Name = "Component 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectComponentsChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(3));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("3"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("component1"));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Component 1 changed"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.Component));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements[1].Id));
            Assert.That(existingElements.Last().Id, Is.EqualTo("testid3"));

       
        }

        [Test]
        public async Task WhenComponentExistsButIsNotInImportThenElementWillBeDeleted()
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
                    StructurizrParentId = "1",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                },
                new Element
                {
                    Id = "testid2",
                    StructurizrId = "2",
                    ParentId = "testid1",
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                },
                new Element
                {
                    Id = "testid3",
                    StructurizrId = "3",
                    ParentId = "testid2",
                    DslId = "component1",
                    Type = ElementType.Component,
                    Name = "Component 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectComponentsChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(2));
            Assert.That(existingElements.First().Id, Is.EqualTo("testid1"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.SoftwareSystem));
            Assert.That(existingElements.Last().Id, Is.EqualTo("testid2"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.Container));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("testid3"));

        }
    }
}
