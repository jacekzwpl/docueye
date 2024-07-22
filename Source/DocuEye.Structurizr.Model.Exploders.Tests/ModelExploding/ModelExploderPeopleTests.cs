using DocuEye.ModelKeeper.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderPeopleTests : BaseExploderTests
    {

        [Test]
        public void WhenPersonIsDefindedThenAllPropertiesAreMached()
        {
            // Arrange
            var people = new List<StructurizrPerson>
            {
                new StructurizrPerson
                {
                    Id = "1",
                    Name = "John Doe",
                    Description = "A person",
                    Location = "London",
                    Url = "http://example.com",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } }
                }
            };
            var exloder = new ModelExploder(this.mapper);
            
            // Act
            var result = exloder.ExplodePeople(people);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().Name, Is.EqualTo("John Doe"));
            Assert.That(result.First().Description, Is.EqualTo("A person"));
            Assert.That(result.First().Location, Is.EqualTo("London"));
            Assert.That(result.First().Url, Is.EqualTo("http://example.com"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(result.First().Properties.Count, Is.EqualTo(2));
            Assert.That(result.First().Properties["key1"], Is.EqualTo("value1"));
            Assert.That(result.First().Properties["key2"], Is.EqualTo("value2"));
            Assert.That(result.First().Type, Is.EqualTo(ElementType.Person));
        }

        [Test]
        public void WhenPeopleAreDefinedThenAllElementsAreExploded()
        {
            // Arrange
            var people = new List<StructurizrPerson>
            {
                new StructurizrPerson
                {
                    Id = "1",
                    Name = "John Doe",
                    Description = "A person",
                    Location = "London",
                    Url = "http://example.com",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } }
                },
                new StructurizrPerson
                {
                    Id = "2",
                    Name = "Jane Doe",
                    Description = "A person",
                    Location = "London",
                    Url = "http://example.com",
                    Tags = "tag1,tag2",
                    Properties = new Dictionary<string, string>() { { "key1", "value1" }, { "key2", "value2" } }
                }
            };
            var exloder = new ModelExploder(this.mapper);

            // Act
            var result = exloder.ExplodePeople(people);

            // Assert
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}