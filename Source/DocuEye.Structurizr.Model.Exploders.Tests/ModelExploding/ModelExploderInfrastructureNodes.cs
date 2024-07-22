using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderInfrastructureNodes : BaseExploderTests
    {
        [Test]
        public void WhenInfrastructureNodeIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var infrastructureNodes = new List<StructurizrInfrastructureNode>
            {
                new StructurizrInfrastructureNode
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
            var result = exloder.ExplodeInfrastructureNodes(infrastructureNodes, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(result.First().Name, Is.EqualTo("InfrastructureNode1"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().Technology, Is.EqualTo("Technology"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(result.First().Url, Is.EqualTo("https://www.docueye.com"));
            Assert.That(result.First().Group, Is.EqualTo("Group1"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.InfrastructureNode));
        }

        [Test]
        public void WhenMultipleInfrastructureNodesAreDefinedThenAllElementsAreExploded()         {
            // Arrange
            var infrastructureNodes = new List<StructurizrInfrastructureNode>()
            {
                new StructurizrInfrastructureNode
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
                new StructurizrInfrastructureNode
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
            var result = exloder.ExplodeInfrastructureNodes(infrastructureNodes, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Name, Is.EqualTo("InfrastructureNode1"));
            Assert.That(result.Last().StructurizrId, Is.EqualTo("2"));
            Assert.That(result.Last().Name, Is.EqualTo("InfrastructureNode2"));
        }
    }
}
