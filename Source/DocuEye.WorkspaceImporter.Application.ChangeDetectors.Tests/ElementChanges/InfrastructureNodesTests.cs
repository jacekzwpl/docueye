using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class InfrastructureNodesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenInfrastructureNodeDoNotExistsThenElementWillBeAdded()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "infrastructure1",
                    Type = ElementType.InfrastructureNode,
                    Name = "Infrastructure Node 1"
                }
            };
            var existingElements = new List<Element>
            {
            
            };
            var detector = new ElementsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectInfrastructureNodesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(existingElements.First().DslId, Is.EqualTo("infrastructure1"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.InfrastructureNode));
            Assert.That(existingElements.First().Name, Is.EqualTo("Infrastructure Node 1"));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));

        }

        [Test]
        public async Task WhenInfrastructureNodeExistsThenElementWillBeUpdated()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "infrastructure1",
                    Type = ElementType.InfrastructureNode,
                    Name = "Infrastructure Node 1 changed"
                }
            };
            var existingElements = new List<Element> {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "1",
                    DslId = "infrastructure1",
                    Type = ElementType.InfrastructureNode,
                    Name = "Infrastructure Node 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectInfrastructureNodesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(existingElements.First().DslId, Is.EqualTo("infrastructure1"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.InfrastructureNode));
            Assert.That(existingElements.First().Name, Is.EqualTo("Infrastructure Node 1 changed"));
            Assert.That(existingElements.First().Id, Is.EqualTo("testid1"));
        }

        [Test]
        public async Task WhenInfrastructureNodeExistsButIsNotInImportThenElementWillBeDeleted()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
            };
            var existingElements = new List<Element> {
                new Element
                {
                    Id = "testid1",
                    StructurizrId = "1",
                    DslId = "infrastructure1",
                    Type = ElementType.InfrastructureNode,
                    Name = "Infrastructure Node 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mediator);

            // Act
            var result = await detector.DetectInfrastructureNodesChanges("workspace1", this.import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(0));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("testid1"));
        }
    }
}
