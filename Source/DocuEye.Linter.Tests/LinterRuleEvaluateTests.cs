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
                Id = "TestRule",
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
            Assert.That(issues.First().Rule.Id, Is.EqualTo("TestRule"));
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
                Id = "FrontendDbConnectionNotAllowed",
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
            Assert.That(issues.First().Rule.Id, Is.EqualTo("FrontendDbConnectionNotAllowed"));
        }

        [Test]
        public void LinterRule_Evaluate_ShouldReturnIssues_ForGeneralRule()
        {
            // Arrange
            var model = new LinterModel()
            {
                Relationships = new List<LinterModelRelationship>
                {
                    new LinterModelRelationship
                    {
                        Source = new LinterModelElement { Identifier = "containerA", Tags = new List<string> { "Container" } },
                        Destination = new LinterModelElement { Identifier = "containerB", Tags = new List<string> { "Container" } },
                        Technology = "HTTP"
                    },
                    new LinterModelRelationship
                    {
                        Source = new LinterModelElement { Identifier = "containerB", Tags = new List<string> { "Container" } },
                        Destination = new LinterModelElement { Identifier = "containerA", Tags = new List<string> { "Container" } },
                        Technology = "HTTP"
                    }
                }
            };

            var evaluationContext = new List<object>();
            var variablesMap = new Dictionary<string, string>();
            variablesMap.Add("@ModelRelationships", "@0");
            evaluationContext.Add(model.Relationships);

            var rule = new LinterRule
            {
                Id = "ContainersCyclingDependency",
                Name = "Containers should not have cyclic dependencies",
                Description = "Containers must not depend on each other in a cyclic manner to avoid tight coupling and maintainability issues.",
                Severity = LinterRuleSeverity.Error,
                Type = LinterRuleType.General,
                Expression = "GeneralIssuesFinder.CyclicDependenciesExists(\"Container\",@ModelRelationships)",
                Enabled = true
            };
            // Act
            var issues = rule.Evaluate(model, evaluationContext, variablesMap);
            // Assert
            Assert.That(issues, Is.Not.Null);
            Assert.That(issues.Count(), Is.EqualTo(1));
            Assert.That(issues.First().Message, Is.EqualTo("Cyclic dependencies discovered: containerA -> containerB -> containerA"));
            Assert.That(issues.First().Rule.Id, Is.EqualTo("ContainersCyclingDependency"));
        }

        [Test]
        public void LinterRule_Evaluate_OnAllModelElementProperties_ShouldReturnIssues()
        {
            var model = new LinterModel()
            {
                Elements = new List<LinterModelElement>
                {
                    new LinterModelElement
                    {
                        Identifier = "1",
                        Name = "Element1",
                        Tags = new List<string> { "Tag1", "Tag3" },
                        Technology = "TechA",
                        Properties = new Dictionary<string, string>
                        {
                            { "Owner", "TeamA" },
                            { "Criticality", "High" }
                        },
                        Description = "This is a critical element owned by TeamA",
                        InstanceOfIdentifier = "BaseElement",
                        ParentIdentifier = "ParentElement"
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
            var expression = "Identifier == \"1\" AND Name == \"Element1\" AND Tags.Contains(\"Tag1\") " +
                             "AND Technology == \"TechA\" " +
                             "AND Properties[\"Owner\"] == \"TeamA\" " +
                             "AND Properties[\"Criticality\"] == \"High\" " +
                             "AND Description.Contains(\"critical\") " +
                             "AND InstanceOfIdentifier == \"BaseElement\" " +
                             "AND ParentIdentifier == \"ParentElement\"";

            var rule = new LinterRule
            {
                Id = "TestRule",
                Name = "Test Rule for Model Elements",
                Description = "This rule checks for elements with Tag1",
                Severity = LinterRuleSeverity.Warning,
                Type = LinterRuleType.ModelElement,
                Expression = expression,
                Enabled = true
            };
            // Act
            var issues = rule.Evaluate(model, new List<object>(), new Dictionary<string, string>());
            // Assert
            Assert.That(issues, Is.Not.Null);
            Assert.That(issues.Count(), Is.EqualTo(1));
            Assert.That(issues.First().Rule.Id, Is.EqualTo("TestRule"));
        }
    }
}
