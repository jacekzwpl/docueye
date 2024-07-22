using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderComponentsTests : BaseExploderTests
    {
        [Test]
        public void WhenComponentIsDefinedThenAllPropertiesAreMached()
        {
            // Arrange
            var components = new List<StructurizrComponent>()
            {
                new StructurizrComponent
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
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeComponents(components, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Name, Is.EqualTo("Component"));
            Assert.That(result.First().Description, Is.EqualTo("Component description"));
            Assert.That(result.First().Technology, Is.EqualTo("Component technology"));
            Assert.That(result.First().Url, Is.EqualTo("Component url"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["Property1"], Is.EqualTo("Value1"));
            Assert.That(result.First().Properties["Property2"], Is.EqualTo("Value2"));
            Assert.That(result.First().StructurizrParentId, Is.EqualTo("parentId"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.Component));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
        }

        [Test]
        public void WhenMultipleComponentsAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var components = new List<StructurizrComponent>()
            {
                new StructurizrComponent
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
                new StructurizrComponent
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
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodeComponents(components, "parentId");

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        } 

       
    }
}
