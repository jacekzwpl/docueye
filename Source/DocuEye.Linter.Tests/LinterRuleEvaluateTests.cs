using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Linter.Tests
{
    public class LinterRuleEvaluateTests
    {
        [Test]
        public void LinterRule_Evaluate_ShouldReturnIssues_ForModelElementRule()
        {
            // Arrange
            var model = new LinterModel()
            {
                Elements = new List<LinterModelElement>
                {
                    new LinterModelElement
                    {
                        Identifier = "1",
                        Name = "Element1",
                        Tags = new List<string> { "Tag1", "Tag3" },
                        Technology = "TechA"
                    },
                    new LinterModelElement
                    {
                        Identifier = "2",
                        Name = "Element2",
                        Tags = new List<string> { "Tag2" },
                        Technology = "TechB"
                    }
                }
            };
            var rule = new LinterRule
            {
                Key = "TestRule",
                Name = "Test Rule for Model Elements",
                Description = "This rule checks for elements with Tag1",
                Severity = LinterRuleSeverity.Warning,
                Type = LinterRuleType.ModelElement,
                Expression = "Tags.Contains(\"Tag1\")",
                Enabled = true
            };
            // Act
            var issues = rule.Evaluate(model, new List<object>(), new Dictionary<string, string>());
            // Assert
            Assert.That(issues, Is.Not.Null);
            Assert.That(issues.Count(), Is.EqualTo(1));
            Assert.That(issues.First().Rule.Key, Is.EqualTo("TestRule"));
        }

        [Test]
        public void LinterRule_Evaluate_ShouldReturnIssues_ForModelRelationshipRule()
        {
            // Arrange
            var model = new LinterModel()
            {
                Relationships = new List<LinterModelRelationship>
                {
                    new LinterModelRelationship
                    {
                        Source = new LinterModelElement { Identifier = "1", Tags = new List<string> { "Frontend" } },
                        Destination = new LinterModelElement { Identifier = "2", Tags = new List<string> { "Database" } },
                        Technology = "HTTP"
                    },
                    new LinterModelRelationship
                    {
                        Source = new LinterModelElement { Identifier = "3", Tags = new List<string> { "Backend" } },
                        Destination = new LinterModelElement { Identifier = "4", Tags = new List<string> { "Cache" } },
                        Technology = "TCP"
                    }
                }
            };
            var rule = new LinterRule
            {
                Key = "FrontendDbConnectionNotAllowed",
                Name = "Frontend can not connect directly to database",
                Description = "Frontend services should not connect directly to the database for security and architecture reasons.",
                Severity = LinterRuleSeverity.Error,
                Type = LinterRuleType.ModelRelationship,
                Expression = "Source.Tags.Contains(\"Frontend\") and Destination.Tags.Contains(\"Database\")",
                Enabled = true
            };
            // Act
            var issues = rule.Evaluate(model, new List<object>(), new Dictionary<string, string>());
            // Assert
            Assert.That(issues, Is.Not.Null);
            Assert.That(issues.Count(), Is.EqualTo(1));
            Assert.That(issues.First().Rule.Key, Is.EqualTo("FrontendDbConnectionNotAllowed"));
        }
    }
}
