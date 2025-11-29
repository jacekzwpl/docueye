using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitDeploymentNodeTests : BaseTests
    {
        [Test]
        public void WhenDefineDeploymentNodeThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentNode ""MyNode"" ""Great Node"" ""Tech"" ""tag1,tag2"" ""1..N""
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Description, Is.EqualTo("Great Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Deployment Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Instances, Is.EqualTo("1..N"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }

        [Test]
        public void WhenDefineDeploymentNodeWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentNode MyNode ""Great Node"" ""Tech"" ""tag1,tag2"" ""1..N""
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Description, Is.EqualTo("Great Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Deployment Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Instances, Is.EqualTo("1..N"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }

        [Test]
        public void WhenDefineDEploymentNodeWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mynode = deploymentNode MyNode ""Great Node"" ""Tech"" ""tag1,tag2"" ""1..N""
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Description, Is.EqualTo("Great Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Deployment Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Instances, Is.EqualTo("1..N"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }

        [Test]
        public void WhenDefineDeploymentNodeWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mynode = deploymentNode ""MyNode"" ""Great Node"" ""Tech"" ""tag1,tag2"" ""1..N""
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Description, Is.EqualTo("Great Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Deployment Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Instances, Is.EqualTo("1..N"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }

        [Test]
        public void WhenDeploymentNodeNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            deploymentNode
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitDeploymentNode(context, "environemnt", null));
        }

        [Test]
        public void WhenDefineDeploymentNodeWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"deploymentNode MyNode {
                description ""Great Node""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", null);
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Description, Is.EqualTo("Great Node"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Deployment Node", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }

        [Test]
        public void WhenDefineDeploymentNodeWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"mynode = deploymentNode MyNode {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitDeploymentNode(context, "environemnt", null);
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenDefineDEploymentNodeWithParentIdentifierAndModelIdentifiersAreHierarhicalThenIdentifierHasParentIdPart()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mynode = deploymentNode ""MyNode""
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical"
            };


            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", "parent");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Identifier, Is.EqualTo("parent.mynode"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }

        [Test]
        public void WhenDefineDEploymentNodeWithOutParentIdentifierAndModelIdentifiersAreHierarhicalThenIdentifierHasEnvironmentPart()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mynode = deploymentNode ""MyNode""
            ");
            var context = parser.deploymentNode();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical"
            };


            // Act
            var result = visitor.VisitDeploymentNode(context, "environemnt", null);

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyNode"));
            Assert.That(result.Identifier, Is.EqualTo("environemnt.mynode"));
            Assert.That(result.DeploymentEnvironmentIdentifier, Is.EqualTo("environemnt"));
        }
    }
}
