using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderContainersTests : BaseExploderTests
    {
        [Test]
        public void WhenContainerIsDefinedThenAllPropertiesAreMached()
        {
            // Arrange
            var containers = new List<StructurizrContainer>()
            {
                new StructurizrContainer
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
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Name, Is.EqualTo("Container"));
            Assert.That(result.First().Description, Is.EqualTo("Container description"));
            Assert.That(result.First().Technology, Is.EqualTo("Container technology"));
            Assert.That(result.First().Url, Is.EqualTo("Container url"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(result.First().Properties["Property2"], Is.EqualTo("Value2"));
            Assert.That(result.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.Container));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
        }

        [Test]
        public void WhenMultipleContainersAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var containers = new List<StructurizrContainer>()
            {
                new StructurizrContainer
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
                new StructurizrContainer
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
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }

        [Test]
        public void WhenChildElementsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var containers = new List<StructurizrContainer>()
            {
                new StructurizrContainer
                {
                    Id = "1",
                    Name = "Container1",
                    Components = new List<StructurizrComponent>
                    {
                        new StructurizrComponent
                        {
                            Id = "3",
                            Name = "Component1"
                        },
                        new StructurizrComponent
                        {
                            Id = "4",
                            Name = "Component2"
                        }
                    }
                },
                new StructurizrContainer
                {
                    Id = "2",
                    Name = "Container2",
                    Components = new List<StructurizrComponent>
                    {
                        new StructurizrComponent
                        {
                            Id = "5",
                            Name = "Component5"
                        },
                        new StructurizrComponent
                        {
                            Id = "6",
                            Name = "Component5"
                        }
                    }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeContainers(containers, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(6));
        }
    }
}
