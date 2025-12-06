using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderDeploymentNodesTests : BaseExploderTests
    {
        [Test]
        public void WhenDeploymentNodeIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var deploymentNode = new StructurizrJsonDeploymentNode
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
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, sourceElements, "parentId");

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
            var sourceElements = new List<ElementToImport>();
            var deploymentNode = new StructurizrJsonDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                Children = new List<StructurizrJsonDeploymentNode>
                {
                    new StructurizrJsonDeploymentNode
                    {
                        Id = "2",
                        Name = "DeploymentNode2",
                        Children = new List<StructurizrJsonDeploymentNode>
                        {
                            new StructurizrJsonDeploymentNode
                            {
                                Id = "3",
                                Name = "DeploymentNode3"
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, sourceElements, "parentId");

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
            var sourceElements = new List<ElementToImport>() { 
                new ElementToImport
                {
                    StructurizrId = "SoftwareSystem1",
                    Type = ElementType.SoftwareSystem
                }
            };
            var deploymentNode = new StructurizrJsonDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                SoftwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>
                {
                    new StructurizrJsonSoftwareSystemInstance
                    {
                        Id = "2",
                        SoftwareSystemId = "SoftwareSystem1"
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, sourceElements, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).StructurizrInstanceOfId, Is.EqualTo("SoftwareSystem1"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.SoftwareSystemInstance));
        }

        [Test]
        public void WhenDeploymentNodeHasContainerInstancesThenAllElementsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>()
            {
                new ElementToImport
                {
                    StructurizrId = "Container1",
                    Type = ElementType.Container
                }
            };
            var deploymentNode = new StructurizrJsonDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                ContainerInstances = new List<StructurizrJsonContainerInstance>
                {
                    new StructurizrJsonContainerInstance
                    {
                        Id = "2",
                        ContainerId = "Container1"
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, sourceElements, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("DeploymentNode1"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.DeploymentNode));
            Assert.That(elements.ElementAt(1).StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.ElementAt(1).StructurizrParentId, Is.EqualTo("1"));
            Assert.That(elements.ElementAt(1).StructurizrInstanceOfId, Is.EqualTo("Container1"));
            Assert.That(elements.ElementAt(1).Type, Is.EqualTo(ElementType.ContainerInstance));
        }

        [Test]
        public void WhenDeploymentNodeHasInfrastructureNodesThenAllElementsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var deploymentNode = new StructurizrJsonDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                InfrastructureNodes = new List<StructurizrJsonInfrastructureNode>
                {
                    new StructurizrJsonInfrastructureNode
                    {
                        Id = "2",
                        Name = "InfrastructureNode1"
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, sourceElements, "parentId");

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
            var sourceElements = new List<ElementToImport>();
            var deploymentNodes = new List<StructurizrJsonDeploymentNode>
            {
                new StructurizrJsonDeploymentNode
                {
                    Id = "1",
                    Name = "DeploymentNode1",
                    Children = new List<StructurizrJsonDeploymentNode>
                    {
                        new StructurizrJsonDeploymentNode
                        {
                            Id = "2",
                            Name = "DeploymentNode2",
                            Children = new List<StructurizrJsonDeploymentNode>
                            {
                                new StructurizrJsonDeploymentNode
                                {
                                    Id = "3",
                                    Name = "DeploymentNode3"
                                }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNodes(deploymentNodes, sourceElements);

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

        [Test]
        public void WhenDeploymentNodeHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var deploymentNode = new StructurizrJsonDeploymentNode
            {
                Id = "1",
                Name = "DeploymentNode1",
                Relationships = new List<StructurizrJsonRelationship>
                {
                    new StructurizrJsonRelationship
                    {
                        Id = "2",
                        SourceId = "1",
                        DestinationId = "3",
                        Description = "Description",
                        Technology = "Technology",
                        Tags = "tag1,tag2",
                        Url = "https://www.docueye.com",
                        Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                            }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNode(deploymentNode, sourceElements, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenMultipleDeploymentNodesHaveRelationshipsThenAllRelationshipsAreExloded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var deploymentNodes = new List<StructurizrJsonDeploymentNode>
            {
                new StructurizrJsonDeploymentNode
                {
                    Id = "1",
                    Name = "DeploymentNode1",
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2"
                        }
                    }
                },
                new StructurizrJsonDeploymentNode
                {
                    Id = "2",
                    Name = "DeploymentNode2",
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "2",
                            SourceId = "2",
                            DestinationId = "3"
                        }
                    },
                    Children = new List<StructurizrJsonDeploymentNode>
                    {
                        new StructurizrJsonDeploymentNode
                        {
                            Id = "3",
                            Name = "DeploymentNode3",
                            Relationships = new List<StructurizrJsonRelationship>
                            {
                                new StructurizrJsonRelationship
                                {
                                    Id = "3",
                                    SourceId = "1",
                                    DestinationId = "3"
                                }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeDeploymentNodes(deploymentNodes, sourceElements);

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(3));
        }
    }
}
