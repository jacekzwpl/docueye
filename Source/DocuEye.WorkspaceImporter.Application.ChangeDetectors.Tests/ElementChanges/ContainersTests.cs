using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class ContainersTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenContainerDoNotExistsThenElementWillBeAdded()
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
                }
            };
            var detector = new ElementsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectContainersChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(2));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("2"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("container1"));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Container 1"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.Container));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements.First().Id));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task WhenContainerExistsThenElementWillBeUpdated()
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
                    Name = "Container 1 changed"
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
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                }
            };

            var detector = new ElementsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectContainersChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(2));
            Assert.That(existingElements.Last().StructurizrId, Is.EqualTo("2"));
            Assert.That(existingElements.Last().DslId, Is.EqualTo("container1"));
            Assert.That(existingElements.Last().Name, Is.EqualTo("Container 1 changed"));
            Assert.That(existingElements.Last().Type, Is.EqualTo(ElementType.Container));
            Assert.That(existingElements.Last().ParentId, Is.EqualTo(existingElements.First().Id));
            Assert.That(existingElements.Last().Id, Is.EqualTo("testid2"));
        }

        [Test]
        public async Task WhenContainerExistsButIsNotInImportThenElementWillBeDeleted() { 
            
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
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
                    DslId = "container1",
                    Type = ElementType.Container,
                    Name = "Container 1"
                }
            };

            var detector = new ElementsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectContainersChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("testid2"));
        }
    }
}
