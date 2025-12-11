using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderContainerInstancesTests : BaseExploderTests
    {
        [Test]
        public void WhenContainerInstanceIsDefinedThenAllPropertiesAreMatched() {
            // Arrange
            var sourceElements = new List<ElementToImport>()
            {
                new ElementToImport
                {
                    StructurizrId = "Container1",
                    StructurizrParentId = "parentId",
                    Type = ElementType.Container,
                    Name = "Container1Name",
                    Description = "Description",
                    Technology = "Technology"
                }
            };
            var containerInstances = new List<StructurizrJsonContainerInstance>
            {
                new StructurizrJsonContainerInstance
                {
                    Id = "1",
                    Tags = "tag1,tag2",
                    ContainerId = "Container1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements,relationships) = exloder.ExplodeContainerInstances(containerInstances, sourceElements, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().StructurizrInstanceOfId, Is.EqualTo("Container1"));
            Assert.That(elements.First().Name, Is.EqualTo("Container1Name"));
            Assert.That(elements.First().Description, Is.EqualTo("Description"));
            Assert.That(elements.First().Technology, Is.EqualTo("Technology"));
            Assert.That(elements.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(elements.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(elements.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(elements.First().Properties.Count, Is.EqualTo(2));
            Assert.That(elements.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(elements.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.ContainerInstance));
        }

        [Test]
        public void WhenMultipleContainerInstancesAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>()
            {
                new ElementToImport
                {
                    StructurizrId = "Container1",
                    StructurizrParentId = "parentId",
                    Type = ElementType.Container,
                    Name = "Container1Name",
                    Description = "Description",
                    Technology = "Technology"
                },
                new ElementToImport
                {
                    StructurizrId = "Container2",
                    StructurizrParentId = "parentId",
                    Type = ElementType.Container,
                    Name = "Container2Name",
                    Description = "Description",
                    Technology = "Technology"
                }
            };
            var containerInstances = new List<StructurizrJsonContainerInstance>()
            {
                new StructurizrJsonContainerInstance
                {
                    Id = "1",
                    Tags = "tag1,tag2",
                    ContainerId = "Container1",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    }
                },
                new StructurizrJsonContainerInstance
                {
                    Id = "2",
                    Tags = "tag1,tag2",
                    ContainerId = "Container2",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainerInstances(containerInstances, sourceElements, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));

        }

        [Test]
        public void WhenContainerInstanceHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var containerInstances = new List<StructurizrJsonContainerInstance>()
            {
                new StructurizrJsonContainerInstance
                {
                    Id = "1",
                    Tags = "tag1,tag2",
                    ContainerId = "Container1",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>()
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
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainerInstances(containerInstances, sourceElements, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
            Assert.That(relationships.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(relationships.First().StructurizrSourceId, Is.EqualTo("1"));
            Assert.That(relationships.First().StructurizrDestinationId, Is.EqualTo("2"));
            Assert.That(relationships.First().Description, Is.EqualTo("Description"));
            Assert.That(relationships.First().Technology, Is.EqualTo("Technology"));
            Assert.That(relationships.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(relationships.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(relationships.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(relationships.First().Url, Is.EqualTo("https://www.docueye.com"));
            Assert.That(relationships.First().Properties.Count, Is.EqualTo(2));
            Assert.That(relationships.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(relationships.First().Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenMultipleContainerInstancesHaveRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var containerInstances = new List<StructurizrJsonContainerInstance>()
            {
                new StructurizrJsonContainerInstance
                {
                    Id = "1",
                    Tags = "tag1,tag2",
                    ContainerId = "Container1",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>()
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
                new StructurizrJsonContainerInstance
                {
                    Id = "2",
                    Tags = "tag1,tag2",
                    ContainerId = "Container2",
                    Properties = new Dictionary<string, string>()
                    { { "key1", "value1" }, { "key2", "value2" }
                    },
                    Relationships = new List<StructurizrJsonRelationship>()
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
                            Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                            }
                        }
                    }
                }
            };
            var exloder = new ModelExploder();

            // Act
            var (elements, relationships) = exloder.ExplodeContainerInstances(containerInstances, sourceElements, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(2));
        }
    }
}
