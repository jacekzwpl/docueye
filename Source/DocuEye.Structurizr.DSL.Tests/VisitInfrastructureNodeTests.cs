using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitInfrastructureNodeTests : BaseTests
    {
        [Test]
        public void WhenDefineInfrastructureNodeThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            infrastructureNode ""MyInfrastructureNode"" ""Great Infrastructure Node"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitInfrastructureNode(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyInfrastructureNode"));
            Assert.That(result.Description, Is.EqualTo("Great Infrastructure Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Infrastructure Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineInfrastructureNodeWithNameWithOutQuotationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            infrastructureNode MyInfrastructureNode ""Great Infrastructure Node"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitInfrastructureNode(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyInfrastructureNode"));
            Assert.That(result.Description, Is.EqualTo("Great Infrastructure Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Infrastructure Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineInfrastructureNodeWithIdentifierAndNameWithOutQuoatationMarksThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = infrastructureNode MyInfrastructureNode ""Great Infrastructure Node"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitInfrastructureNode(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyInfrastructureNode"));
            Assert.That(result.Description, Is.EqualTo("Great Infrastructure Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Infrastructure Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineInfrastructureNodeWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystem = infrastructureNode ""MyInfrastructureNode"" ""Great Infrastructure Node"" ""Tech"" ""tag1,tag2""
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();

            // Act
            var result = visitor.VisitInfrastructureNode(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyInfrastructureNode"));
            Assert.That(result.Description, Is.EqualTo("Great Infrastructure Node"));
            Assert.That(result.Technology, Is.EqualTo("Tech"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Infrastructure Node", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("mysystem"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenInfrastructureNodeNameIsNotSetThenExceptionIsThrown()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            infrastructureNode
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();

            // Act & Assert
            Assert.Throws<Exception>(() => visitor.VisitInfrastructureNode(context, "parentId"));
        }

        [Test]
        public void WhenDefineInfrastructureNodeWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"infrastructureNode MyInfrastructureNode {
                description ""Great Infrastructure Node""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();
            // Act
            var result = visitor.VisitInfrastructureNode(context, "parentId");
            // Assert
            Assert.That(result.Name, Is.EqualTo("MyInfrastructureNode"));
            Assert.That(result.Description, Is.EqualTo("Great Infrastructure Node"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Infrastructure Node", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineInfrastructureNodeWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"myinfrastructureNode = infrastructureNode MyInfrastructureNode {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem")
                }
            };
            // Act
            visitor.VisitInfrastructureNode(context, "parentId");
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenDefineInfrastructureNodeWithIdentifierAndModelIdentifiersAreHierarhicalThenIdentifierHasParentIdPart()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            myinfrastructureNode = infrastructureNode ""MyInfrastructureNode""
            ");
            var context = parser.infrastructureNode();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical"
            };


            // Act
            var result = visitor.VisitInfrastructureNode(context, "parentId");

            // Assert
            Assert.That(result.Name, Is.EqualTo("MyInfrastructureNode"));
            Assert.That(result.Identifier, Is.EqualTo("parentId.myinfrastructureNode"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }
    }
}
