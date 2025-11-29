using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderSoftwareSystemsTests : BaseExploderTests
    {
        [Test]
        public void WhenSoftwareSystemIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var softwareSystems = new List<StructurizrJsonSoftwareSystem>()
            {
                new StructurizrJsonSoftwareSystem
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
            var (elements, relationships) = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().Name, Is.EqualTo("SoftwareSystem"));
            Assert.That(elements.First().Description, Is.EqualTo("SoftwareSystem description"));
            Assert.That(elements.First().Technology, Is.Null);
            Assert.That(elements.First().Url, Is.EqualTo("SoftwareSystem url"));
            Assert.That(elements.First().Properties.Count, Is.EqualTo(2));
            Assert.That(elements.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(elements.First().Properties["Property2"], Is.EqualTo("Value2"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.SoftwareSystem));
            Assert.That(elements.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(elements.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(elements.First().Tags?.Last(), Is.EqualTo("tag2"));
        }

        [Test]
        public void WhenMultipleSoftwareSystemsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var softwareSystems = new List<StructurizrJsonSoftwareSystem>()
            {
                new StructurizrJsonSoftwareSystem
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
                new StructurizrJsonSoftwareSystem
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
            var (elements, relationships) = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenChildElementsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var softwareSystems = new List<StructurizrJsonSoftwareSystem>()
            {
                new StructurizrJsonSoftwareSystem
                {
                    Id = "1",
                    Name = "SoftwareSystem1",
                    Containers = new List<StructurizrJsonContainer>
                    {
                        new StructurizrJsonContainer
                        {
                            Id = "1",
                            Name = "Container1"
                        }
                    }
                },
                new StructurizrJsonSoftwareSystem
                {
                    Id = "2",
                    Name = "SoftwareSystem2",
                    Containers = new List<StructurizrJsonContainer>
                    {
                        new StructurizrJsonContainer
                        {
                            Id = "2",
                            Name = "Container2",
                            Components = new List<StructurizrJsonComponent>
                            {
                                new StructurizrJsonComponent
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
            var (elements, relationships) = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(5));
        }

        [Test]
        public void WhenSoftwareSystemHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var softwareSystems = new List<StructurizrJsonSoftwareSystem>()
            {
                new StructurizrJsonSoftwareSystem
                {
                    Id = "1",
                    Name = "SoftwareSystem1",
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Relationship1 description",
                            Technology = "Technology1",
                            Tags = "tag1,tag2",
                            Properties = new Dictionary<string, string>
                            {
                                { "Property1", "Value1" },
                                { "Property2", "Value2" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
            Assert.That(relationships.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(relationships.First().StructurizrSourceId, Is.EqualTo("1"));
            Assert.That(relationships.First().StructurizrDestinationId, Is.EqualTo("2"));
            Assert.That(relationships.First().Description, Is.EqualTo("Relationship1 description"));
            Assert.That(relationships.First().Technology, Is.EqualTo("Technology1"));
            Assert.That(relationships.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(relationships.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(relationships.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(relationships.First().Properties.Count, Is.EqualTo(2));
            Assert.That(relationships.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(relationships.First().Properties["Property2"], Is.EqualTo("Value2"));
        }

        [Test]
        public void WhenMultipleSoftwareSystemsHaveRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var softwareSystems = new List<StructurizrJsonSoftwareSystem>()
            {
                new StructurizrJsonSoftwareSystem
                {
                    Id = "1",
                    Name = "SoftwareSystem1",
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Relationship1 description",
                            Technology = "Technology1",
                            Tags = "tag1,tag2",
                            Properties = new Dictionary<string, string>
                            {
                                { "Property1", "Value1" },
                                { "Property2", "Value2" }
                            }
                        }
                    }
                },
                new StructurizrJsonSoftwareSystem
                {
                    Id = "2",
                    Name = "SoftwareSystem2",
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "2",
                            SourceId = "2",
                            DestinationId = "3",
                            Description = "Relationship2 description",
                            Technology = "Technology2",
                            Tags = "tag3,tag4",
                            Properties = new Dictionary<string, string>
                            {
                                { "Property3", "Value3" },
                                { "Property4", "Value4" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeSoftwareSystems(softwareSystems);

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(2));
        }
    }
}
