using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class PeopleTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenPersonDoNotExistsThenElementWillBeAdded()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "person1",
                    Type = ElementType.Person,
                    Name = "Person 1"
                }
            };
            var existingElements = new List<Element>();
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectPeopleChanges("workspace1", import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(existingElements.First().DslId, Is.EqualTo("person1"));
            Assert.That(existingElements.First().Name, Is.EqualTo("Person 1"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.Person));

            Assert.That(result.ElementsToAdd.Count(), Is.EqualTo(1));
        }

        [Test]
        public async Task WhenPersonExistsThenElementWillBeUpdated()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>
            {
                new ElementToImport
                {
                    StructurizrId = "1",
                    DslId = "person1",
                    Type = ElementType.Person,
                    Name = "Person 1 changed"
                }
            };
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "personID1",
                    StructurizrId = "1",
                    DslId = "person1",
                    Type = ElementType.Person,
                    Name = "Person 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectPeopleChanges("workspace1", import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(1));
            Assert.That(existingElements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(existingElements.First().DslId, Is.EqualTo("person1"));
            Assert.That(existingElements.First().Name, Is.EqualTo("Person 1 changed"));
            Assert.That(existingElements.First().Type, Is.EqualTo(ElementType.Person));
            Assert.That(existingElements.First().Id, Is.EqualTo("personID1"));
        }

        [Test]
        public async Task WhenPersonExistsButIsNotInImportThenElementWillBeDeleted()
        {
            // Arrange
            var elementsToImport = new List<ElementToImport>();
            var existingElements = new List<Element>
            {
                new Element
                {
                    Id = "personID1",
                    StructurizrId = "1",
                    DslId = "person1",
                    Type = ElementType.Person,
                    Name = "Person 1"
                }
            };
            var detector = new ElementsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = await detector.DetectPeopleChanges("workspace1", import, elementsToImport, existingElements);

            // Assert
            Assert.That(existingElements.Count(), Is.EqualTo(0));

            Assert.That(result.ElementsToDelete.Count(), Is.EqualTo(1));
            Assert.That(result.ElementsToDelete.First(), Is.EqualTo("personID1"));
        }
    }
}
