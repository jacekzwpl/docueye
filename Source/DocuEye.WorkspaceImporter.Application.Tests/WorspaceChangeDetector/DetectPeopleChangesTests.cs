﻿using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Application.Services.ModelExploder;
using DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector;

namespace DocuEye.WorkspaceImporter.Application.Tests.WorspaceChangeDetector
{
    public class DetectPeopleChangesTests : BaseWorkspaceChangeDetectorTests
    {
        [Test]
        public async Task WhenThereIsNoChangesThenDetectorReturnsNoElementsChanged()
        {
            // Arrange

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be any elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenThereIsNewPersonThenDetectorReturnsElementToAdd()
        {
            // Arrange
            this.explodedElements.Add(new ExplodedElement()
            {
                StructurizrId = "P3",
                DslId = "P3",
                Name = "Person3",
                Type = ElementType.Person,
            });

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should be 1 element to add in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "P3").Count(), Is.EqualTo(1), "There should be 1 element with dslid = P3 to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be any elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");
        }
        [Test]
        public async Task WhenThereAreChangesInPersonDataThenDetectorReturnsElementToChange()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "P2")
                .Name = "Person name changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(1), "There should not be 1 element to change in detector results.");
            Assert.That(result.ElementsToChange?.Where(o => o.DslId == "P2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = P2 to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(0), "There should not be any elements to delete in detector results.");

        }
        [Test]
        public async Task WhenDslIdOfPersonChangedThenDetectorReturnsElementToAddAndDelete()
        {
            // Arrange
            this.explodedElements.First(o => o.DslId == "P2")
                .DslId = "P2Changed";

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(1), "There should not be 1 element to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToAdd?.Where(o => o.DslId == "P2Changed").Count(), Is.EqualTo(1), "There should be 1 element with dslid = P2Changed to add in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "P2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = P2 to delete in detector results.");
        }

        [Test]
        public async Task WhenPersonHasBeenRemovedThenDetectorReturnsElementToDelete()
        {
            // Arrange
            var item = this.explodedElements.First(o => o.DslId == "P2");
            this.explodedElements.Remove(item);

            // Act
            var detector = new WorkspaceChangeDetectorService(this.mapper, this.mediator);
            var result = await detector.DetectElementsChanges("wId", this.import, this.explodedElements, this.existingElements);

            // Assert
            Assert.That(result.ElementsToAdd?.Count, Is.EqualTo(0), "There should not be no elements to add in detector results.");
            Assert.That(result.ElementsToChange?.Count, Is.EqualTo(0), "There should not be no elements to change in detector results.");
            Assert.That(result.ElementsToDelete?.Count, Is.EqualTo(1), "There should not be 1 element to delete in detector results.");
            Assert.That(result.ElementsToDelete?.Where(o => o.DslId == "P2").Count(), Is.EqualTo(1), "There should be 1 element with dslid = P2 to delete in detector results.");
        }
    }
}
