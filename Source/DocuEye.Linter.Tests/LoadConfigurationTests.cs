using DocuEye.Linter.Model;
using Microsoft.Extensions.Logging.Abstractions;

namespace DocuEye.Linter.Tests
{
    public class LoadConfigurationTests
    {
        [Test]
        public void LoadConfiguration_ShouldLoadAllConfigurationOptions()
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
            linter.LoadConfiguration(config);

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
        public void LoadConfiguration_ShouldThrowException_ForUnsupportedRuleType()
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
            var ex = Assert.Throws<Exception>(() => linter.LoadConfiguration(config));
            Assert.That(ex.Message, Is.EqualTo("Unsupported rule type: 'InvalidType' for rule with key: 'InvalidRuleType'"));
        }

        [Test]
        public void LoadConfiguration_ShouldThrowException_ForUnsupportedRuleSeverity()
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
            var ex = Assert.Throws<Exception>(() => linter.LoadConfiguration(config));
            Assert.That(ex.Message, Is.EqualTo("Unsupported rule severity: 'Critical' for rule with key: 'InvalidRuleSeverity'"));
        }
    }
}
