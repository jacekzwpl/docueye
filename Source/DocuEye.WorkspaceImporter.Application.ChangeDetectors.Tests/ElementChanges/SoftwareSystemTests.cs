using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class SoftwareSystemTests : BaseDetectorsTests
    {

        [Test]
        public async Task WhenSoftwareSystemDoNotExistsThenElementWillBeAdded()
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
                }
            };
            var existingElements = new List<Element>
            {

            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectSoftwareSystemsChanges("workspace1", import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(existingElements.First().DslId, Is.EqualTo("softwaresystem1"));
            Assert.That(existingElements.First().Name, Is.EqualTo("Software System 1"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.SoftwareSystem));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task WhenSoftwareSystemExistsThenElementWillBeUpdated()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1 changed"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "systemID1",
                    StructurizrId = "old1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectSoftwareSystemsChanges("workspace1", import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(existingElements.First().DslId, Is.EqualTo("softwaresystem1"));
            Assert.That(existingElements.First().Name, Is.EqualTo("Software System 1 changed"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.SoftwareSystem));

        }

        [Test]
        public async Task WhenSoftwareSystemExistsButIsNotInImportThenElementWillBeDeleted()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {

            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "old1",
                    StructurizrId = "old1",
                    DslId = "softwaresystem1",
                    Type = ElementType.SoftwareSystem,
                    Name = "Software System 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectSoftwareSystemsChanges("workspace1", import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(0));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("old1"));
        }


    }
}
