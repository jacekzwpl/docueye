using DocuEye.Linter.Model;
using Microsoft.Extensions.Logging.Abstractions;

namespace DocuEye.Linter.Tests
{
    public class LinterAnalyzeTests
    {
        [Test]
        public async Task Linter_Analyze_ShouldReturnFalse_BasedOnConfiguration()
        {
            // Arrange
            var model = new LinterModel()
            {
                Elements = new List<LinterModelElement>
                {
                    new LinterModelElement
                    {
                        Identifier = "1",
                        Name = "FrontendService",
                        Tags = new List<string> { "Frontend" },
                        Technology = "ASP.NET"
                    },
                    new LinterModelElement
                    {
                        Identifier = "2",
                        Name = "Database",
                        Tags = new List<string> { "Database" },
                        Technology = "SQLServer"
                    }
                },
                Relationships = new List<LinterModelRelationship>
                {
                    new LinterModelRelationship
                    {
                        Source = new LinterModelElement
                        {
                            Identifier = "1",
                            Name = "FrontendService",
                            Tags = new List<string> { "Frontend" },
                            Technology = "ASP.NET"
                        },
                        Destination = new LinterModelElement
                        {
                            Identifier = "2",
                            Name = "Database",
                            Tags = new List<string> { "Database" },
                            Technology = "SQLServer"
                        },
                        Technology = "DirectConnection"
                    }
                }
            };
            var logger = NullLogger<ArchitectureLinter>.Instance;
            var linter = new ArchitectureLinter(model, logger);
            var config = @"
                {
                    ""MaxAllowedSeverity"": ""Warning"",
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
            await linter.LoadConfiguration(config);
            // Act
            var result = linter.Analyze();
            // Assert
            Assert.That(result, Is.EqualTo(false));
            Assert.That(linter.Issues.Count(), Is.EqualTo(1));
            Assert.That(linter.Issues.First().Rule.Id, Is.EqualTo("FrontendDbConnectionNotAllowed"));
        }
    }
}
