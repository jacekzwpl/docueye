using DocuEye.Structurizr.Json.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Model.Exploders.Tests.DocsExploding
{
    public class ExplodeDecisionsTests : BaseExploderTests
    {
        [Test]
        public void WhenDecisionForElementExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var decisions = new List<StructurizrJsonDecision>
            {
                new StructurizrJsonDecision
                {
                    Content = "content",
                    Date = "2021-01-01",
                    Status = "Proposed",
                    Title = "title",
                    Id = "id",
                    Format = "markdown",
                    ElementId = "elementId",
                    Links = new List<StructurizrJsonDecisionLink>
                    {
                        new StructurizrJsonDecisionLink
                        {
                            Id = "linkId",
                            Description = "description"
                        }
                    }
                    
                }
            };
            var exploder = new DocumentationExploder();

            // Act
            var result = exploder.ExplodeDecisions(decisions, "documentationId", "elementId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Content, Is.EqualTo("content"));
            Assert.That(result.First().Date, Is.EqualTo("2021-01-01"));
            Assert.That(result.First().Status, Is.EqualTo("Proposed"));
            Assert.That(result.First().Title, Is.EqualTo("title"));
            Assert.That(result.First().StrucuturizrId, Is.EqualTo("id"));
            Assert.That(result.First().Format, Is.EqualTo("markdown"));
            Assert.That(result.First().StrucuturizrElementId, Is.EqualTo("elementId"));
            Assert.That(result.First().Links?.Count(), Is.EqualTo(1));
            Assert.That(result.First().Links?.First().StructurizrId, Is.EqualTo("linkId"));
            Assert.That(result.First().Links?.First().Description, Is.EqualTo("description"));
            Assert.That(result.First().DocumentationId, Is.EqualTo("documentationId"));

        }

        [Test]
        public void WhenDecisionForWorkspaceExistsThenAllPropertiesAreMatched()
        {
            // Arrange
            var decisions = new List<StructurizrJsonDecision>
            {
                new StructurizrJsonDecision
                {
                    Content = "content",
                    Date = "2021-01-01",
                    Status = "Proposed",
                    Title = "title",
                    Id = "id",
                    Format = "markdown",
                    ElementId = "elementId",
                    Links = new List<StructurizrJsonDecisionLink>
                    {
                        new StructurizrJsonDecisionLink
                        {
                            Id = "linkId",
                            Description = "description"
                        }
                    }

                }
            };
            var exploder = new DocumentationExploder();

            // Act
            var result = exploder.ExplodeDecisions(decisions, "documentationId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().Content, Is.EqualTo("content"));
            Assert.That(result.First().Date, Is.EqualTo("2021-01-01"));
            Assert.That(result.First().Status, Is.EqualTo("Proposed"));
            Assert.That(result.First().Title, Is.EqualTo("title"));
            Assert.That(result.First().StrucuturizrId, Is.EqualTo("id"));
            Assert.That(result.First().Format, Is.EqualTo("markdown"));
            Assert.That(result.First().StrucuturizrElementId, Is.Null);
            Assert.That(result.First().Links?.Count(), Is.EqualTo(1));
            Assert.That(result.First().Links?.First().StructurizrId, Is.EqualTo("linkId"));
            Assert.That(result.First().Links?.First().Description, Is.EqualTo("description"));
            Assert.That(result.First().DocumentationId, Is.EqualTo("documentationId"));

        }

        [Test]
        public void WhenMultipleDecisionsExistsThenAllElementsAreExploded()
        {
            // Arrange
            var decisions = new List<StructurizrJsonDecision>
            {
                new StructurizrJsonDecision
                {
                    Content = "content",
                    Date = "2021-01-01",
                    Status = "Proposed",
                    Title = "title",
                    Id = "id",
                    Format = "markdown",
                    ElementId = "elementId",
                    Links = new List<StructurizrJsonDecisionLink>
                    {
                        new StructurizrJsonDecisionLink
                        {
                            Id = "linkId",
                            Description = "description"
                        }
                    }
                },
                new StructurizrJsonDecision
                {
                    Content = "content",
                    Date = "2021-01-01",
                    Status = "Proposed",
                    Title = "title",
                    Id = "id",
                    Format = "markdown",
                    ElementId = "elementId",
                    Links = new List<StructurizrJsonDecisionLink>
                    {
                        new StructurizrJsonDecisionLink
                        {
                            Id = "linkId",
                            Description = "description"
                        }
                    }
                }
            };
            var exploder = new DocumentationExploder();

            // Act
            var result = exploder.ExplodeDecisions(decisions, "documentationId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}
