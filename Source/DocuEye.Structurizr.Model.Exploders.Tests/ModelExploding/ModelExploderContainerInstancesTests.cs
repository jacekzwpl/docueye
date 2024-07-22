using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderContainerInstancesTests : BaseExploderTests
    {
        [Test]
        public void WhenContainerInstanceIsDefinedThenAllPropertiesAreMatched() {
            // Arrange
            var containerInstances = new List<StructurizrContainerInstance>
            {
                new StructurizrContainerInstance
                {
                    Id = "1",
                    Tags = "tag1,tag2",
                    ContainerId = "Container1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeContainerInstances(containerInstances, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(result.First().StructurizrInstanceOfId, Is.EqualTo("Container1"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.ContainerInstance));
        }

        [Test]
        public void WhenMultipleContainerInstancesAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var containerInstances = new List<StructurizrContainerInstance>()
            {
                new StructurizrContainerInstance
                {
                    Id = "1",
                    Tags = "tag1,tag2",
                    ContainerId = "Container1",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    }
                },
                new StructurizrContainerInstance
                {
                    Id = "2",
                    Tags = "tag1,tag2",
                    ContainerId = "Container2",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeContainerInstances(containerInstances, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));

        }
    }
}
