using DocuEye.Structurizr.DSL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.DslToJson.Tests
{
    public class ConvertDeploymentViewTests
    {
        private readonly StructurizrWorkspace workspace = new StructurizrWorkspace()
        {
            Model = new StructurizrModel()
            {
                Elements = new List<StructurizrModelElement>()
                {
                    new StructurizrModelElement("1","s1")
                    {
                        Name = "SoftwareSystem1",
                        Type = StructurizrModelElementType.SoftwareSystem
                    },
                    new StructurizrModelElement("2","s2")
                    {
                        Name = "SoftwareSystem2",
                        Type = StructurizrModelElementType.SoftwareSystem
                    },
                    new StructurizrModelElement("3","s3")
                    {
                        Name = "SoftwareSystem3",
                        Type = StructurizrModelElementType.SoftwareSystem
                    },
                    new StructurizrModelElement("4","c1")
                    {
                        Name = "Container1",
                        Type = StructurizrModelElementType.Container,
                        ParentIdentifier = "s1"
                    },
                    new StructurizrModelElement("5","c2")
                    {
                        Name = "Container2",
                        Type = StructurizrModelElementType.Container,
                        ParentIdentifier = "s2"
                    },
                    new StructurizrModelElement("6","dn1")
                    {
                        Name = "Server1",
                         DeploymentEnvironmentIdentifier = "Production",
                        Type = StructurizrModelElementType.DeploymentNode
                    },
                    new StructurizrModelElement("7","dn2")
                    {
                        Name = "Server2",
                        DeploymentEnvironmentIdentifier = "Production",
                        Type = StructurizrModelElementType.DeploymentNode
                    },
                    new StructurizrModelElement("8","dn3")
                    {
                        Name = "Server3",
                        DeploymentEnvironmentIdentifier = "Production",
                        Type = StructurizrModelElementType.DeploymentNode
                    },
                    new StructurizrModelElement("9","ci1")
                    {
                        Name = "Container1",
                         DeploymentEnvironmentIdentifier = "Production",
                        Type = StructurizrModelElementType.ContainerInstance,
                        InstanceOfIdentifier = "c1",
                        ParentIdentifier = "dn1",
                    },
                    new StructurizrModelElement("10","ci2")
                    {
                        Name = "Container2",
                        DeploymentEnvironmentIdentifier = "Production",
                        InstanceOfIdentifier = "c2",
                        Type = StructurizrModelElementType.ContainerInstance,
                        ParentIdentifier = "dn2",
                    },
                    new StructurizrModelElement("11","si3")
                    {
                        Name = "System3",
                        DeploymentEnvironmentIdentifier = "Production",
                        InstanceOfIdentifier = "s3",
                        Type = StructurizrModelElementType.SoftwareSystemInstance,
                        ParentIdentifier = "dn3",
                    }
                },
                Relationships = new List<StructurizrRelationship>()
                {
                    new StructurizrRelationship("id1", new string[] { "tag1" })
                    {
                        ModelId = "1",
                        SourceIdentifier = "ci1",
                        DestinationIdentifier = "ci2"
                    },
                    new StructurizrRelationship("id2", new string[] { "tag1" })
                    {
                        ModelId = "2",
                        SourceIdentifier = "ci1",
                        DestinationIdentifier = "si3"
                    }
                }
            }

        };

        [Test]
        public void WhenViewWithAllScopeIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrDeploymentView("*", "Production", "ViewKey", "Test Description")
            {
                Title = "Test Title",
                Include = new string[] { "*" }
            };
            // Act
            var result = workspaceConverter.ConvertDeploymentView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SoftwareSystemId, Is.Null);
            Assert.That(result.Key, Is.EqualTo("ViewKey"));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Elements, Is.Not.Null);
            Assert.That(result.Elements.Count(), Is.EqualTo(6));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenViewWithSystemScopeIsConvertedThenValidJsonIsReturned()
        {
            // Arrange
            var workspaceConverter = new WorkspaceConverter(this.workspace, "");
            var view = new StructurizrDeploymentView("s1", "Production", "ViewKey", "Test Description")
            {
                Title = "Test Title",
                Include = new string[] { "*" }
            };
            // Act
            var result = workspaceConverter.ConvertDeploymentView(view);
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.SoftwareSystemId, Is.EqualTo("1"));
            Assert.That(result.Key, Is.EqualTo("ViewKey"));
            Assert.That(result.Title, Is.EqualTo("Test Title"));
            Assert.That(result.Description, Is.EqualTo("Test Description"));
            Assert.That(result.Elements, Is.Not.Null);
            Assert.That(result.Elements.Count(), Is.EqualTo(4));
            Assert.That(result.Relationships?.Count(), Is.EqualTo(1));
        }
    }
}
