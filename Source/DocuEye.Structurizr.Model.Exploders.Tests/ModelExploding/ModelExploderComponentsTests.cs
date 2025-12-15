using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderComponentsTests : BaseExploderTests
    {
        [Test]
        public void WhenComponentIsDefinedThenAllPropertiesAreMached()
        {
            // Arrange
            var components = new List<StructurizrJsonComponent>()
            {
                new StructurizrJsonComponent
                {
                    Id = "1",
                    Name = "Component",
                    Description = "Component description",
                    Technology = "Component technology",
                    Url = "Component url",
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
            var (elements, relationships) = exloder.ExplodeComponents(components, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().Name, Is.EqualTo("Component"));
            Assert.That(elements.First().Description, Is.EqualTo("Component description"));
            Assert.That(elements.First().Technology, Is.EqualTo("Component technology"));
            Assert.That(elements.First().Url, Is.EqualTo("Component url"));
            Assert.That(elements.First().Properties.Count, Is.EqualTo(2));
            Assert.That(elements.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(elements.First().Properties["Property2"], Is.EqualTo("Value2"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.Component));
            Assert.That(elements.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(elements.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(elements.First().Tags?.Last(), Is.EqualTo("tag2"));
        }

        [Test]
        public void WhenMultipleComponentsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var components = new List<StructurizrJsonComponent>()
            {
                new StructurizrJsonComponent
                {
                    Id = "1",
                    Name = "Component1",
                    Description = "Component1 description",
                    Technology = "Component1 technology",
                    Url = "Component1 url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    }
                },
                new StructurizrJsonComponent
                {
                    Id = "2",
                    Name = "Component2",
                    Description = "Component2 description",
                    Technology = "Component2 technology",
                    Url = "Component2 url",
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
            var (elements,relationships) = exloder.ExplodeComponents(components, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenComponentHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var components = new List<StructurizrJsonComponent>()
            {
                new StructurizrJsonComponent
                {
                    Id = "1",
                    Name = "Component1",
                    Description = "Component1 description",
                    Technology = "Component1 technology",
                    Url = "Component1 url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>()
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Relationship description",
                            Technology = "Relationship technology",
                            Url = "Relationship url",
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
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeComponents(components, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
        }

        [Test]
        public void WhenMultipleComponentsHaveRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var components = new List<StructurizrJsonComponent>()
            {
                new StructurizrJsonComponent
                {
                    Id = "1",
                    Name = "Component1",
                    Description = "Component1 description",
                    Technology = "Component1 technology",
                    Url = "Component1 url",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property1", "Value1" },
                        { "Property2", "Value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>()
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "1",
                            SourceId = "1",
                            DestinationId = "2",
                            Description = "Relationship description",
                            Technology = "Relationship technology",
                            Url = "Relationship url",
                            Tags = "tag1,tag2",
                            Properties = new Dictionary<string, string>
                            {
                                { "Property1", "Value1" },
                                { "Property2", "Value2" }
                            }
                        }
                    }
                },
                new StructurizrJsonComponent
                {
                    Id = "2",
                    Name = "Component2",
                    Description = "Component2 description",
                    Technology = "Component2 technology",
                    Url = "Component2 url",
                    Tags = "tag3,tag4",
                    Properties = new Dictionary<string, string>
                    {
                        { "Property3", "Value3" },
                        { "Property4", "Value4" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>()
                    {
                        new StructurizrJsonRelationship
                        {
                            Id = "2",
                            SourceId = "2",
                            DestinationId = "3",
                            Description = "Relationship description",
                            Technology = "Relationship technology",
                            Url = "Relationship url",
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
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeComponents(components, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(2));
        }


    }
}
