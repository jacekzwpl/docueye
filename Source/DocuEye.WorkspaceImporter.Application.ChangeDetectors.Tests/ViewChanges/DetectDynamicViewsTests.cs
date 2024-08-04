﻿using DocuEye.ModelKeeper.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ViewChanges
{
    public class DetectDynamicViewsTests : BaseDetectorsTests
    {
        [Test]
        public void WhenDynamicViewIsDefindedThenAllPropertiesAreMatched()
        {
            // Arrange
            var viewsToImport = new List<ViewToImport>()
            {
                new ViewToImport()
                {
                    Key = "view1",
                    ViewType = ViewType.DynamicView,
                    Elements = new List<ElementInViewToImport>()
                    {
                        new ElementInViewToImport()
                        {
                            StructurizrId = "1",
                            X = 1,
                            Y = 2
                        },
                        new ElementInViewToImport()
                        {
                            StructurizrId = "2",
                            X = 3,
                            Y = 4
                        }
                    },
                    Relationships = new List<RelationshipInViewToImport>()
                    {
                        new RelationshipInViewToImport()
                        {
                            StructurizrId = "1",
                            Order = "1.0",
                            Response = false,
                            Description = "Description"
                        },
                        new RelationshipInViewToImport()
                        {
                            StructurizrId = "1",
                            Order = "1.1",
                            Response = true,
                            Description = "Description"
                        }
                    },
                    PaperSize = "A4",
                    ExternalBoundariesVisible = true,
                    StructurizrElementId = "1",
                    Title = "Title",
                    Tags = new List<string>() { "Tag1", "Tag2" },
                    Description = "Description",
                    AutomaticLayout = new ViewAutomaticLayout()
                    {
                        RankDirection = "TopBottom"
                    }
                }
            };
            var viewsIdsMap = new Dictionary<string, string>()
            {
                { "view1", "viewid1" }
            };
            var existingElements = new List<Element>()
            {
                new Element()
                {
                    Id = "element1",
                    StructurizrId = "1"
                },
                new Element()
                {
                    Id = "element2",
                    StructurizrId = "2"
                }
            };
            var existingRelationships = new List<Relationship>()
            {
                new Relationship()
                {
                    Id = "relation1",
                    StructurizrId = "1"
                }
            };
            var elementDiagrams = new Dictionary<string, string>()
            {
                { "element1", "viewid1" }
            };
            var detector = new ViewsChangeDetector(this.mapper, this.mediator);

            // Act
            var result = detector.DetectDynamicViews(
                "workspaceId",
                viewsToImport,
                viewsIdsMap,
                existingElements,
                existingRelationships,
                elementDiagrams);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().Id, Is.EqualTo("viewid1"));
            Assert.That(result.First().WorkspaceId, Is.EqualTo("workspaceId"));
            Assert.That(result.First().Elements.Count, Is.EqualTo(2));
            Assert.That(result.First().Relationships.Count, Is.EqualTo(2));
            Assert.That(result.First().PaperSize, Is.EqualTo("A4"));
            Assert.That(result.First().ElementId, Is.EqualTo("element1"));
            Assert.That(result.First().Title, Is.EqualTo("Title"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().ExternalBoundariesVisible, Is.EqualTo(true));
            Assert.That(result.First().AutomaticLayout?.RankDirection, Is.EqualTo("TopBottom"));
        }
    }
}
