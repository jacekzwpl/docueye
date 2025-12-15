using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderContainersTests : BaseExploderTests
    {
        [Test]
        public void WhenContainerIsDefinedThenAllPropertiesAreMached()
        {
            // Arrange
            var containers = new List<StructurizrJsonContainer>()
            {
                new StructurizrJsonContainer
                {
                    Id = "1",
                    Name = "Container",
                    Description = "Container description",
                    Technology = "Container technology",
                    Url = "Container url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().Name, Is.EqualTo("Container"));
            Assert.That(elements.First().Description, Is.EqualTo("Container description"));
            Assert.That(elements.First().Technology, Is.EqualTo("Container technology"));
            Assert.That(elements.First().Url, Is.EqualTo("Container url"));
            Assert.That(elements.First().Properties.Count, Is.EqualTo(2));
            Assert.That(elements.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(elements.First().Properties["Property2"], Is.EqualTo("Value2"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.Container));
            Assert.That(elements.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(elements.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(elements.First().Tags?.Last(), Is.EqualTo("tag2"));
        }

        [Test]
        public void WhenMultipleContainersAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var containers = new List<StructurizrJsonContainer>()
            {
                new StructurizrJsonContainer
                {
                    Id = "1",
                    Name = "Container1",
                    Description = "Container1 description",
                    Technology = "Container1 technology",
                    Url = "Container1 url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    }
                },
                new StructurizrJsonContainer
                {
                    Id = "2",
                    Name = "Container2",
                    Description = "Container2 description",
                    Technology = "Container2 technology",
                    Url = "Container2 url",
                    Tags = "tag3,tag4",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property3", "Value3" },
                        { "Property4", "Value4" }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenChildElementsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var containers = new List<StructurizrJsonContainer>()
            {
                new StructurizrJsonContainer
                {
                    Id = "1",
                    Name = "Container1",
                    Components = new List<StructurizrJsonComponent>
                    {
                        new StructurizrJsonComponent
                        {
                            Id = "3",
                            Name = "Component1"
                        },
                        new StructurizrJsonComponent
                        {
                            Id = "4",
                            Name = "Component2"
                        }
                    }
                },
                new StructurizrJsonContainer
                {
                    Id = "2",
                    Name = "Container2",
                    Components = new List<StructurizrJsonComponent>
                    {
                        new StructurizrJsonComponent
                        {
                            Id = "5",
                            Name = "Component5"
                        },
                        new StructurizrJsonComponent
                        {
                            Id = "6",
                            Name = "Component5"
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(6));
        }

        [Test]
        public void WhenMultipleContainersHaveRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var containers = new List<StructurizrJsonContainer>()
            {
                new StructurizrJsonContainer
                {
                    Id = "1",
                    Name = "Container1",
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
                            Properties = new Dictionary<string, string>
                            {
                                { "Property1", "Value1" },
                                { "Property2", "Value2" }
                            }
                        }
                    }
                },
                new StructurizrJsonContainer
                {
                    Id = "2",
                    Name = "Container2",
                    Relationships = new List<StructurizrJsonRelationship>
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "2",
                            SourceId = "2",
                            DestinationId = "3",
                            Description = "Description",
                            Technology = "Technology",
                            Tags = "tag1,tag2",
                            Url = "https://www.docueye.com",
                            Properties = new Dictionary<string, string>
                            {
                                { "Property1", "Value1" },
                                { "Property2", "Value2" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenContainerHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var containers = new List<StructurizrJsonContainer>()
            {
                new StructurizrJsonContainer
                {
                    Id = "1",
                    Name = "Container1",
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
                            Properties = new Dictionary<string, string>
                            {
                                { "Property1", "Value1" },
                                { "Property2", "Value2" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
        }
    }
}
