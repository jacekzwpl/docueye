using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDeploymentEnvironmentTests : BaseTests
    {
        [Test]
        public void WhenDefineDeploymentEnvironmentThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentEnvironment ""MyEnvironemnt""
            ");
            var context = parser.deploymentEnvironment();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrDeploymentEnvironment)visitor.VisitDeploymentEnvironment(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyEnvironemnt"));
            Assert.That(result.Identifier, Is.Not.Null);
            
        }

        [Test]
        public void WhenDefineDeploymentEnvironmentWithIdThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            myenv = deploymentEnvironment ""MyEnvironemnt""
            ");
            var context = parser.deploymentEnvironment();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrDeploymentEnvironment)visitor.VisitDeploymentEnvironment(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyEnvironemnt"));
            Assert.That(result.Identifier, Is.EqualTo("myenv"));

        }

        [Test]
        public void WhenDefineDeplymentEnvironmentWithChildsThenAllExist()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            myenv = deploymentEnvironment ""MyEnvironemnt"" {
                deploymentNode ""MyNode""
            }
            ");
            var context = parser.deploymentEnvironment();
            var visitor = new DSLVisitor();

            // Act
            var result = (StructurizrDeploymentEnvironment)visitor.VisitDeploymentEnvironment(context);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyEnvironemnt"));
            Assert.That(result.Identifier, Is.EqualTo("myenv"));
            Assert.That(visitor.Workspace.Model.Elements.Count, Is.EqualTo(1));
            Assert.That(visitor.Workspace.Model.Elements.First().Name, Is.EqualTo("MyNode"));
        }
    }
}
