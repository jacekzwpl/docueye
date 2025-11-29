using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDeploymentGroupTests : BaseTests
    {
        [Test]
        public void WhenDefineDeploymentGroupThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"deploymentGroup mygroup 
            ");
            var context = parser.deploymentGroup();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDeploymentGroup(context, "environment");
            // Assert
            Assert.That(result.Identifier, !Is.Null);
            Assert.That(result.Name, Is.EqualTo("mygroup"));
            Assert.That(result.DeploymentEnvironemntIdentifier, Is.EqualTo("environment"));
        }

        [Test]
        public void WhenDefineDeploymentGroupWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"groupid = deploymentGroup mygroup 
            ");
            var context = parser.deploymentGroup();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDeploymentGroup(context, "environment");
            // Assert
            Assert.That(result.Identifier, Is.EqualTo("groupid"));
            Assert.That(result.Name, Is.EqualTo("mygroup"));
            Assert.That(result.DeploymentEnvironemntIdentifier, Is.EqualTo("environment"));
        }

        [Test]
        public void WhenDefineDeploymentGroupWithIdentifiersHierarhicalThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"groupid = deploymentGroup mygroup 
            ");
            var context = parser.deploymentGroup();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical"
            };
            // Act
            var result = visitor.VisitDeploymentGroup(context, "environment");
            // Assert
            Assert.That(result.Identifier, Is.EqualTo("environment.groupid"));
            Assert.That(result.Name, Is.EqualTo("mygroup"));
            Assert.That(result.DeploymentEnvironemntIdentifier, Is.EqualTo("environment"));
        }
    }
}
