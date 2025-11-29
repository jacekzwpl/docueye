using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderInfrastructureNodes : BaseExploderTests
    {
        [Test]
        public void WhenInfrastructureNodeIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var infrastructureNodes = new List<StructurizrJsonInfrastructureNode>
            {
                new StructurizrJsonInfrastructureNode
                {
                    Id = "1",
                    Description = "Description",
                    Environment = "Development",
                    Technology = "Technology",
                    Tags = "tag1,tag2",
                    Name = "InfrastructureNode1",
                    Url = "https://www.docueye.com",
                    Group = "Group1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeInfrastructureNodes(infrastructureNodes, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Name, Is.EqualTo("InfrastructureNode1"));
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
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.InfrastructureNode));
        }

        [Test]
        public void WhenMultipleInfrastructureNodesAreDefinedThenAllElementsAreExploded()         {
            // Arrange
            var infrastructureNodes = new List<StructurizrJsonInfrastructureNode>()
            {
                new StructurizrJsonInfrastructureNode
                {
                    Id = "1",
                    Description = "Description",
                    Environment = "Development",
                    Technology = "Technology",
                    Tags = "tag1,tag2",
                    Name = "InfrastructureNode1",
                    Url = "https://www.docueye.com",
                    Group = "Group1",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    }
                },
                new StructurizrJsonInfrastructureNode
                {
                    Id = "2",
                    Description = "Description",
                    Environment = "Development",
                    Technology = "Technology",
                    Tags = "tag1,tag2",
                    Name = "InfrastructureNode2",
                    Url = "https://www.docueye.com",
                    Group = "Group1",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeInfrastructureNodes(infrastructureNodes, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().Name, Is.EqualTo("InfrastructureNode1"));
            Assert.That(elements.Last().StructurizrId, Is.EqualTo("2"));
            Assert.That(elements.Last().Name, Is.EqualTo("InfrastructureNode2"));
        }

        [Test]
        public void WhenInfrastructureNodeHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var infrastructureNodes = new List<StructurizrJsonInfrastructureNode>
            {
                new StructurizrJsonInfrastructureNode
                {
                    Id = "1",
                    Description = "Description",
                    Environment = "Development",
                    Technology = "Technology",
                    Tags = "tag1,tag2",
                    Name = "InfrastructureNode1",
                    Url = "https://www.docueye.com",
                    Group = "Group1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Description",
                            Technology = "Technology",
                            Tags = "tag1,tag2",
                            Url = "https://www.docueye.com",
                            Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeInfrastructureNodes(infrastructureNodes, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenMultipleInfrastructureNodesHaveRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var infrastructureNodes = new List<StructurizrJsonInfrastructureNode>
            {
                new StructurizrJsonInfrastructureNode
                {
                    Id = "1",
                    Description = "Description",
                    Environment = "Development",
                    Technology = "Technology",
                    Tags = "tag1,tag2",
                    Name = "InfrastructureNode1",
                    Url = "https://www.docueye.com",
                    Group = "Group1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Description",
                            Technology = "Technology",
                            Tags = "tag1,tag2",
                            Url = "https://www.docueye.com",
                            Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                            }
                        }
                    }
                },
                new StructurizrJsonInfrastructureNode
                {
                    Id = "2",
                    Description = "Description",
                    Environment = "Development",
                    Technology = "Technology",
                    Tags = "tag1,tag2",
                    Name = "InfrastructureNode1",
                    Url = "https://www.docueye.com",
                    Group = "Group1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "2",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Description",
                            Technology = "Technology",
                            Tags = "tag1,tag2",
                            Url = "https://www.docueye.com",
                            Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeInfrastructureNodes(infrastructureNodes, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(2));
        }
    }
}
