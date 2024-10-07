﻿using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderDeploymentNodesTests : BaseExploderTests
    {
        [Test]
        public void WhenDeploymentNodeIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var deploymentNode = new StructurizrDeploymentNode
            {
                Id = "1",
                Description = "Description",
                Environment = "Development",
                Technology = "Technology",
                Tags = "tag1,tag2",
                Name = "DeploymentNode1",
                Url = "https://www.docueye.com",
                Group = "Group1",
                Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Description, Is.EqualTo("Description"));
            Assert.That(elements.First().Technology, Is.EqualTo("Technology"));
            Assert.That(elements.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(elements.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(elements.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(elements.First().Url, Is.EqualTo("https://www.docueye.com"));
            Assert.That(elements.First().Group, Is.EqualTo("Group1"));
            Assert.That(elements.First().Properties.Count, Is.EqualTo(2));
            Assert.That(elements.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(elements.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
        }

        [Test]
        public void WhenDeploymentNodeHasChildrenThenAllElementsAreExploded()
        {
            // Arrange
            var deploymentNode = new StructurizrDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                Children = new List<StructurizrDeploymentNode>
                {
                    new StructurizrDeploymentNode
                    {
                        Id = "2",
                        Name = "DeploymentNode2",
                        Children = new List<StructurizrDeploymentNode>
                        {
                            new StructurizrDeploymentNode
                            {
                                Id = "3",
                                Name = "DeploymentNode3"
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(3));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).Name, Is.EqualTo("DeploymentNode2"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(2).StructurizrId, Is.EqualTo("3"));
            Assert.That(elements.ElementAt(2).StructurizrParentId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(2).Name, Is.EqualTo("DeploymentNode3"));
            Assert.That(elements.ElementAt(2).Type, Is.EqualTo(ElementType.DeploymentNode));
        }

        [Test]
        public void WhenDeploymentNodeHasSoftwareSystemInstancesThenAllElementsAreExploded()
        {
            // Arrange
            var deploymentNode = new StructurizrDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                SoftwareSystemInstances = new List<StructurizrSoftwareSystemInstance>
                {
                    new StructurizrSoftwareSystemInstance
                    {
                        Id = "2",
                        SoftwareSystemId = "SoftwareSystemInstance1"
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).StructurizrInstanceOfId, Is.EqualTo("SoftwareSystemInstance1"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.SoftwareSystemInstance));
        }

        [Test]
        public void WhenDeploymentNodeHasContainerInstancesThenAllElementsAreExploded()
        {
            // Arrange
            var deploymentNode = new StructurizrDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                ContainerInstances = new List<StructurizrContainerInstance>
                {
                    new StructurizrContainerInstance
                    {
                        Id = "2",
                        ContainerId = "ContainerInstance1"
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).StructurizrInstanceOfId, Is.EqualTo("ContainerInstance1"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.ContainerInstance));
        }

        [Test]
        public void WhenDeploymentNodeHasInfrastructureNodesThenAllElementsAreExploded()
        {
            // Arrange
            var deploymentNode = new StructurizrDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                InfrastructureNodes = new List<StructurizrInfrastructureNode>
                {
                    new StructurizrInfrastructureNode
                    {
                        Id = "2",
                        Name = "InfrastructureNode1"
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).Name, Is.EqualTo("InfrastructureNode1"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.InfrastructureNode));
        }

        [Test]
        public void WhenMultipleDeploymentNodesAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var deploymentNodes = new List<StructurizrDeploymentNode>
            {
                new StructurizrDeploymentNode
                {
                    Id = "1",
                    Name = "DeploymentNode1",
                    Children = new List<StructurizrDeploymentNode>
                    {
                        new StructurizrDeploymentNode
                        {
                            Id = "2",
                            Name = "DeploymentNode2",
                            Children = new List<StructurizrDeploymentNode>
                            {
                                new StructurizrDeploymentNode
                                {
                                    Id = "3",
                                    Name = "DeploymentNode3"
                                }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNodes(deploymentNodes);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(3));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().StructurizrParentId, Is.Null);
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).Name, Is.EqualTo("DeploymentNode2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(2).StructurizrId, Is.EqualTo("3"));
            Assert.That(elements.ElementAt(2).Name, Is.EqualTo("DeploymentNode3"));
            Assert.That(elements.ElementAt(2).StructurizrParentId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(2).Type, Is.EqualTo(ElementType.DeploymentNode));
        }
    }
}
