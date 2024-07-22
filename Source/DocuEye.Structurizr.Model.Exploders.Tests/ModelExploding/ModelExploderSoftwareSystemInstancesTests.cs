using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderSoftwareSystemInstancesTests : BaseExploderTests
    {
        [Test]
        public void WhenSoftwareSystemInstanceIsDefinedThenAllPropertiesAreMached()
        {
            // Arrange
            var softwareSystemInstances = new List<StructurizrSoftwareSystemInstance>
            {
                new StructurizrSoftwareSystemInstance {
                    Id = "1",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);
            
            // Act
            var result = exloder.ExplodeSoftwareSystemInstances(softwareSystemInstances, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(result.First().StructurizrInstanceOfId, Is.EqualTo("System1"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.SoftwareSystemInstance));
        }

        [Test]
        public void WhenMultipleSoftwareSystemInstancesAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var softwareSystemInstances = new List<StructurizrSoftwareSystemInstance>()
            {
                new StructurizrSoftwareSystemInstance {
                    Id = "1",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                },
                new StructurizrSoftwareSystemInstance {
                    Id = "2",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System2",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeSoftwareSystemInstances(softwareSystemInstances, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));

        }
    }

    
}

