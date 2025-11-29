using DocuEye.ModelKeeper.Model;
using DocuEye.Structurizr.Json.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderSoftwareSystemInstancesTests : BaseExploderTests
    {
        [Test]
        public void WhenSoftwareSystemInstanceIsDefinedThenAllPropertiesAreMached()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>() { 
                new ElementToImport { 
                    StructurizrId = "System1", 
                    Type = ElementType.SoftwareSystem,
                    Name = "System1Name",
                    Description = "System1Description",
                    Technology = "System1Technology",
                }
            };
            var softwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>
            {
                new StructurizrJsonSoftwareSystemInstance {
                    Id = "1",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeSoftwareSystemInstances(softwareSystemInstances, sourceElements, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(1));
            Assert.That(elements.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(elements.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(elements.First().StructurizrInstanceOfId, Is.EqualTo("System1"));
            Assert.That(elements.First().Name, Is.EqualTo("System1Name"));
            Assert.That(elements.First().Description, Is.EqualTo("System1Description"));
            Assert.That(elements.First().Technology, Is.EqualTo("System1Technology"));
            Assert.That(elements.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(elements.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(elements.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(elements.First().Properties.Count, Is.EqualTo(2));
            Assert.That(elements.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(elements.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(elements.First().Type, Is.EqualTo(ElementType.SoftwareSystemInstance));
        }

        [Test]
        public void WhenMultipleSoftwareSystemInstancesAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>() { 
                new ElementToImport { StructurizrId = "System1", Type = ElementType.SoftwareSystem },
                new ElementToImport { StructurizrId = "System2", Type = ElementType.SoftwareSystem }
            };
            var softwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>()
            {
                new StructurizrJsonSoftwareSystemInstance {
                    Id = "1",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                },
                new StructurizrJsonSoftwareSystemInstance {
                    Id = "2",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System2",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeSoftwareSystemInstances(softwareSystemInstances, sourceElements, "parentId");

            // Assert
            Assert.That(elements.Count(), Is.EqualTo(2));

        }

        [Test]
        public void WhenSoftwareSystemInstanceHasRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var softwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>()
            {
                new StructurizrJsonSoftwareSystemInstance {
                    Id = "1",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
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
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeSoftwareSystemInstances(softwareSystemInstances, sourceElements, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(1));
            Assert.That(relationships.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(relationships.First().StructurizrSourceId, Is.EqualTo("1"));
            Assert.That(relationships.First().StructurizrDestinationId, Is.EqualTo("2"));
            Assert.That(relationships.First().Description, Is.EqualTo("Description"));
            Assert.That(relationships.First().Technology, Is.EqualTo("Technology"));
            Assert.That(relationships.First().Url, Is.EqualTo("https://www.docueye.com"));
            Assert.That(relationships.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(relationships.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(relationships.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(relationships.First().Properties.Count, Is.EqualTo(2));
            Assert.That(relationships.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(relationships.First().Properties["key2"], Is.EqualTo("value2"));
        }

        [Test]
        public void WhenMultipleSoftwareSystemInstancesHaveRelationshipsThenAllRelationshipsAreExploded()
        {
            // Arrange
            var sourceElements = new List<ElementToImport>();
            var softwareSystemInstances = new List<StructurizrJsonSoftwareSystemInstance>()
            {
                new StructurizrJsonSoftwareSystemInstance {
                    Id = "1",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System1",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
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
                new StructurizrJsonSoftwareSystemInstance {
                    Id = "2",
                    Tags = "tag1,tag2",
                    SoftwareSystemId = "System2",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" }
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
            var exloder = new ModelExploder(this.mapper);

            // Act
            var (elements, relationships) = exloder.ExplodeSoftwareSystemInstances(softwareSystemInstances, sourceElements, "parentId");

            // Assert
            Assert.That(relationships.Count(), Is.EqualTo(2));

        }

    }
}

