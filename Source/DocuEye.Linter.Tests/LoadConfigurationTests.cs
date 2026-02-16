using DocuEye.Linter.Model;
using Microsoft.Extensions.Logging.Abstractions;

namespace DocuEye.Linter.Tests
{
    public class LoadConfigurationTests
    {
        [Test]
        public async Task LoadConfigurationFromFile_ShouldLoadAllConfigurationOptions()
        {
            // Arrange
            var config = @"
                {
                    ""MaxAllowedSeverity"": ""Warning"",
                    ""Rules"": []
                }
            ";
            var linter = new ArchitectureLinter(
                new LinterModel(), 
                NullLogger<ArchitectureLinter>.Instance,
                new HttpClient(new GetFileHttpMessageHandler("./TestData/test-linter-config.json","application/json")));
            // Act
            await linter.LoadConfigurationFromFile("http://localhost/test-config.json");
            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(linter.Configuration.MaxAllowedSeverity, Is.EqualTo(LinterRuleSeverity.Warning));
            Assert.That(linter.Configuration.Rules.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task LoadConfigurationWithExtends_ShouldLoadAllConfigurationOptions()
        {
            // Arrange
            var config = @"
                {
                    ""Extends"": ""./TestData/test-linter-config.json"",
                    ""MaxAllowedSeverity"": ""Information"",
                    ""Rules"": [
                        {
                            ""Id"": ""DE-ARCH-001"",
                            ""Severity"": ""Warning""
                        }
                    ]
                }
            ";
            var linter = new ArchitectureLinter(new LinterModel(), NullLogger<ArchitectureLinter>.Instance);
            // Act
            await linter.LoadConfiguration(config);
            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(linter.Configuration.MaxAllowedSeverity, Is.EqualTo(LinterRuleSeverity.Information));
            Assert.That(linter.Configuration.Rules.Count(), Is.EqualTo(2));
            Assert.That(linter.Configuration.Rules.First(r => r.Id == "DE-ARCH-001").Severity, Is.EqualTo(LinterRuleSeverity.Warning));
        }

        [Test]
        public async Task LoadConfiguration_ShouldLoadAllConfigurationOptions()
        {
            // Arrange
            var config = @"
                {
                    ""MaxAllowedSeverity"": ""Error"",
                    ""Rules"": [
                        {
                            ""Id"": ""FrontendDbConnectionNotAllowed"",
                            ""Name"": ""Frontend can not connect directly to database"",
                            ""Description"": ""Frontend services should not connect directly to the database for security and architecture reasons."",
                            ""Severity"": ""Error"",
                            ""Type"": ""ModelRelationship"",
                            ""Expression"": ""Source.Tags.Contains(\""Frontend\"") and Destination.Tags.Contains(\""Database\"")"",
                            ""HelpLink"": ""https://example.com/rules/frontend-db-connection-not-allowed""
                        }
                    ]
                }
            ";
            var linter = new ArchitectureLinter(new LinterModel(), NullLogger<ArchitectureLinter>.Instance);

            // Act
            await linter.LoadConfiguration(config);

            // Assert
            Assert.That(config, Is.Not.Null);
            Assert.That(linter.Configuration.MaxAllowedSeverity, Is.EqualTo(LinterRuleSeverity.Error));
            Assert.That(linter.Configuration.Rules.Count(), Is.EqualTo(1));
            Assert.That(linter.Configuration.Rules.First().Id, Is.EqualTo("FrontendDbConnectionNotAllowed"));
            Assert.That(linter.Configuration.Rules.First().Name, Is.EqualTo("Frontend can not connect directly to database"));
            Assert.That(linter.Configuration.Rules.First().Description, Is.EqualTo("Frontend services should not connect directly to the database for security and architecture reasons."));
            Assert.That(linter.Configuration.Rules.First().Severity, Is.EqualTo(LinterRuleSeverity.Error));
            Assert.That(linter.Configuration.Rules.First().Type, Is.EqualTo("ModelRelationship"));
            Assert.That(linter.Configuration.Rules.First().Expression, Is.EqualTo("Source.Tags.Contains(\"Frontend\") and Destination.Tags.Contains(\"Database\")"));
            Assert.That(linter.Configuration.Rules.First().HelpLink, Is.EqualTo("https://example.com/rules/frontend-db-connection-not-allowed"));
            Assert.That(linter.Configuration.Rules.First().Enabled, Is.EqualTo(true));
        }

        [Test]
        public async Task LoadConfiguration_ShouldThrowException_ForUnsupportedRuleType()
        {
            // Arrange
            var config = @"
                {
                    ""MaxAllowedSeverity"": ""Warning"",
                    ""Rules"": [
                        {
                            ""Id"": ""InvalidRuleType"",
                            ""Name"": ""Invalid Rule Type Example"",
                            ""Severity"": ""Warning"",
                            ""Type"": ""InvalidType"",
                            ""Expression"": ""SomeExpression""
                        }
                    ]
                }
            ";
            var linter = new ArchitectureLinter(new LinterModel(), NullLogger<ArchitectureLinter>.Instance);
            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await linter.LoadConfiguration(config));
            Assert.That(ex.Message, Is.EqualTo("Unsupported rule type: 'InvalidType' for rule with id: 'InvalidRuleType'"));
        }

        [Test]
        public async Task LoadConfiguration_ShouldThrowException_ForUnsupportedRuleSeverity()
        {
            // Arrange
            var config = @"
                {
                    ""MaxAllowedSeverity"": ""Warning"",
                    ""Rules"": [
                        {
                            ""Id"": ""InvalidRuleSeverity"",
                            ""Name"": ""Invalid Rule Severity Example"",
                            ""Severity"": ""Critical"",
                            ""Type"": ""ModelElement"",
                            ""Expression"": ""SomeExpression""
                        }
                    ]
                }
            ";
            var linter = new ArchitectureLinter(new LinterModel(), NullLogger<ArchitectureLinter>.Instance);
            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(async () => await linter.LoadConfiguration(config));
            Assert.That(ex.Message, Is.EqualTo("Unsupported rule severity: 'Critical' for rule with id: 'InvalidRuleSeverity'"));
        }

        [Test]
        public async Task LoadConfiguration_WithMultipleVariables_ShouldLoadCorrectly()
        {
            // Arrange
            var config = @"
                {
                    ""MaxAllowedSeverity"": ""Warning"",
                    ""Variables"": {
                        ""StringArrayVariable"": [""Value1"", ""Value2"", ""Value3""],
                        ""IntArrayVariable"": [1, 2, 3],
                        ""StringVariable"": ""SomeStringValue"",
                        ""NumberVariable"": 42,
                        ""BooleanVariable"": true,
                        ""ObjectVariable"": { ""Key1"": ""Value1"", ""Key2"": ""2"" }
                    },
                    ""Rules"": [
                        {
                            ""Id"": ""RuleWithoutEnabled"",
                            ""Name"": ""Rule Without Enabled Property"",
                            ""Severity"": ""Warning"",
                            ""Type"": ""ModelElement"",
                            ""Expression"": ""SomeExpression""
                        }
                    ]
                }
            ";
            var linter = new ArchitectureLinter(new LinterModel(), NullLogger<ArchitectureLinter>.Instance);
            // Act
            await linter.LoadConfiguration(config);
            // Assert

            Assert.That(linter.Configuration.Variables.Keys.Count(), Is.EqualTo(6));
            Assert.That(linter.Configuration.Variables["StringArrayVariable"], Is.EquivalentTo(new string[] { "Value1", "Value2", "Value3" }));
            Assert.That(linter.Configuration.Variables["IntArrayVariable"], Is.EquivalentTo(new int[] { 1, 2, 3 }));
            Assert.That(linter.Configuration.Variables["StringVariable"], Is.EqualTo("SomeStringValue"));
            Assert.That(linter.Configuration.Variables["NumberVariable"], Is.EqualTo(42));
            Assert.That(linter.Configuration.Variables["BooleanVariable"], Is.EqualTo(true));
            Assert.That(linter.Configuration.Variables["ObjectVariable"], Is.EquivalentTo(new Dictionary<string, object> { { "Key1", "Value1" }, { "Key2", "2" } }));
        }
    }
}
