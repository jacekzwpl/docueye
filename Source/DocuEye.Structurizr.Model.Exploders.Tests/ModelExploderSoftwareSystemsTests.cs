using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests
{
    public class ModelExploderSoftwareSystemsTests : BaseExploderTests
    {
        [Test]
        public void WhenSoftwareSystemIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var softwareSystems = new List<StructurizrSoftwareSystem>()
            {
                new StructurizrSoftwareSystem
                {
                    Id = "1",
                    Name = "SoftwareSystem",
                    Description = "SoftwareSystem description",
                    Url = "SoftwareSystem url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Name, Is.EqualTo("SoftwareSystem"));
            Assert.That(result.First().Description, Is.EqualTo("SoftwareSystem description"));
            Assert.That(result.First().Technology, Is.Null);
            Assert.That(result.First().Url, Is.EqualTo("SoftwareSystem url"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(result.First().Properties["Property2"], Is.EqualTo("Value2"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.SoftwareSystem));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
        }

        [Test]
        public void WhenMultipleSoftwareSystemsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var softwareSystems = new List<StructurizrSoftwareSystem>()
            {
                new StructurizrSoftwareSystem
                {
                    Id = "1",
                    Name = "SoftwareSystem1",
                    Description = "SoftwareSystem1 description",
                    Url = "SoftwareSystem1 url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    }
                },
                new StructurizrSoftwareSystem
                {
                    Id = "2",
                    Name = "SoftwareSystem2",
                    Description = "SoftwareSystem2 description",
                    Url = "SoftwareSystem2 url",
                    Tags = "tag3,tag4",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property3", "Value3" },
                        { "Property4", "Value4" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenChildElementsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var softwareSystems = new List<StructurizrSoftwareSystem>()
            {
                new StructurizrSoftwareSystem
                {
                    Id = "1",
                    Name = "SoftwareSystem1",
                    Containers = new List<StructurizrContainer>
                    {
                        new StructurizrContainer
                        {
                            Id = "1",
                            Name = "Container1"
                        }
                    }
                },
                new StructurizrSoftwareSystem
                {
                    Id = "2",
                    Name = "SoftwareSystem2",
                    Containers = new List<StructurizrContainer>
                    {
                        new StructurizrContainer
                        {
                            Id = "2",
                            Name = "Container2",
                            Components = new List<StructurizrComponent>
                            {
                                new StructurizrComponent
                                {
                                    Id = "1",
                                    Name = "Component1"
                                }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(5));
        }
    }
}
