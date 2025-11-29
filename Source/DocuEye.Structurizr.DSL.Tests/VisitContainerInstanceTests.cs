using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DSL.Tests
{
    public class VisitContainerInstanceTests : BaseTests
    {
        [Test]
        public void WhenDefineContainerInstanceThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"containerInstance mysystem ""deploymentGroup1,deploymentGroup2"" ""tag1,tag2""
            ");
            var context = parser.containerInstance();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "mysystem")
                },
                DeploymentGroups = new List<StructurizrDeploymentGroup>()
                {
                    new StructurizrDeploymentGroup("deploymentGroup1", "name1", "environemnt"),
                    new StructurizrDeploymentGroup("deploymentGroup2", "name1", "environemnt")
                }
            };

            // Act
            var result = visitor.VisitContainerInstance(context, "parentId");

            // Assert
            Assert.That(result.ContainerIdentifier, Is.EqualTo("mysystem"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container Instance", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
            Assert.That(result.DeploymentGroupsIdentiifiers, Is.EquivalentTo(new[] { "deploymentGroup1", "deploymentGroup2" }));
        }

        [Test]
        public void WhenDefineContainerInstanceWithIdentifierThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            myinstance = containerInstance mysystem.test ""deploymentGroup1,deploymentGroup2"" ""tag1,tag2""
            ");
            var context = parser.containerInstance();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "mysystem.test")
                },
                DeploymentGroups = new List<StructurizrDeploymentGroup>()
                {
                    new StructurizrDeploymentGroup("deploymentGroup1", "name1", "environemnt"),
                    new StructurizrDeploymentGroup("deploymentGroup2", "name1", "environemnt")
                }
            };

            // Act
            var result = visitor.VisitContainerInstance(context, "parentId");

            // Assert
            Assert.That(result.ContainerIdentifier, Is.EqualTo("mysystem.test"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container Instance", "tag1", "tag2" }));
            Assert.That(result.Identifier, Is.EqualTo("myinstance"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
            Assert.That(result.DeploymentGroupsIdentiifiers, Is.EquivalentTo(new[] { "deploymentGroup1", "deploymentGroup2" }));


        }

        [Test]
        public void WhenDefineContainerInstanceWithBodyThenAllPropertiesAreSet()
        {
            // Arrange
            var parser = CreateParserFromText(@"containerInstance mysystem {
                description ""Great Infrastructure Node""
                tags ""tag1""
                properties {
                    ""key1"" ""value1""
                    ""key2"" ""value2""
                }
                url https://docueye.com
            }
            ");
            var context = parser.containerInstance();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "mysystem")
                }
            };
            // Act
            var result = visitor.VisitContainerInstance(context, "parentId");
            // Assert
            Assert.That(result.ContainerIdentifier, Is.EqualTo("mysystem"));
            Assert.That(result.Description, Is.EqualTo("Great Infrastructure Node"));
            Assert.That(result.Tags, Is.EquivalentTo(new[] { "Element", "Container Instance", "tag1" }));
            Assert.That(result.Identifier, Is.Not.Null);
            Assert.That(result.Properties.Count, Is.EqualTo(2));
            Assert.That(result.Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.Url, Is.EqualTo("https://docueye.com"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }

        [Test]
        public void WhenDefineContainerInstanceWithRelationshipsThenRelationshipsExists()
        {
            // Arrange
            var parser = CreateParserFromText(@"mysystemInstance = containerInstance mysystem {
               -> othersystem ""description"" ""technology"" ""tag1,tag2""
            }
            ");
            var context = parser.containerInstance();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "othersystem"),
                    new StructurizrModelElement("2", "mysystem")
                }
            };
            // Act
            visitor.VisitContainerInstance(context, "parentId");
            // Assert
            Assert.That(visitor.Workspace.Model.Relationships.Count, Is.EqualTo(1));
        }

        [Test]
        public void WhenDefineContainerInstanceWithIdentifierAndModelIdentifiersAreHierarhicalThenIdentifierHasParentIdPart()
        {
            // Arrange
            var parser = CreateParserFromText(@"
            mysystemInstance = containerInstance mysystem
            ");
            var context = parser.containerInstance();
            var visitor = new DSLVisitor();
            visitor.Workspace.Model = new StructurizrModel()
            {
                Identifiers = "hierarchical",
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1", "mysystem")
                }
            };


            // Act
            var result = visitor.VisitContainerInstance(context, "parentId");

            // Assert
            Assert.That(result.ContainerIdentifier, Is.EqualTo("mysystem"));
            Assert.That(result.Identifier, Is.EqualTo("parentId.mysystemInstance"));
            Assert.That(result.DeploymentNodeIdentifier, Is.EqualTo("parentId"));
        }
    }
}
