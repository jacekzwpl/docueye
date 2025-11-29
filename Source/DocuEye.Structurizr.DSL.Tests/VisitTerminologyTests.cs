using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitTerminologyTests : BaseTests
    {
        [Test]
        public void WhenTerminologyIsDefinedThenCorrectValuesAreReturned()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            terminology {
                person ""Person""
                softwareSystem ""Software System""
                container ""Container""
                component ""Component""
                relationship ""Relationship""
                deploymentNode ""Deployment Node""
                infrastructureNode ""Infrastructure Node""
            }
            ");
            var context = parser.terminology();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitTerminology(context);
            // Assert
            Assert.That(result.Person, Is.EqualTo("Person"));
            Assert.That(result.SoftwareSystem, Is.EqualTo("Software System"));
            Assert.That(result.Container, Is.EqualTo("Container"));
            Assert.That(result.Component, Is.EqualTo("Component"));
            Assert.That(result.Relationship, Is.EqualTo("Relationship"));
            Assert.That(result.DeploymentNode, Is.EqualTo("Deployment Node"));
            Assert.That(result.InfrastructureNode, Is.EqualTo("Infrastructure Node"));
        }
    }
}
