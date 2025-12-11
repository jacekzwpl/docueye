using DocuEye.Structurizr.Json.Model;

namespace DocuEye.Structurizr.Model.Exploders.Tests.ModelExploding
{
    public class ModelExploderExplodeRelationshipsTests : BaseExploderTests
    {
        [Test]
        public void WhenRelationshipIsDefinedThenAllPropertiesAreMatched()
        {
            // Arrange
            var relationships = new List<StructurizrJsonRelationship>
                        {
                            new StructurizrJsonRelationship
                            {
                                Id = "1",
                                SourceId = "1",
                                DestinationId = "2",
                                Description = "Description",
                                Technology = "Technology",
                                InteractionStyle = "Synchronous",
                                LinkedRelationshipId = "3",
                                Tags = "tag1,tag2"
                            }
                        };
            var exploder = new ModelExploder();

            // Act
            var result = exploder.ExplodeRelationships(relationships);

            // Assert
            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrSourceId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrDestinationId, Is.EqualTo("2"));
            Assert.That(result.First().Description, Is.EqualTo("Description"));
            Assert.That(result.First().Technology, Is.EqualTo("Technology"));
            Assert.That(result.First().InteractionStyle, Is.EqualTo("Synchronous"));
            Assert.That(result.First().Tags?.Count(), Is.EqualTo(2));
            Assert.That(result.First().Tags?.First(), Is.EqualTo("tag1"));
            Assert.That(result.First().Tags?.Last(), Is.EqualTo("tag2"));
            Assert.That(result.First().StructurizrLinkedRelationshipId, Is.EqualTo("3"));

        }

        [Test]
        public void WhenMultipleRelationshipsAreDEfinedThenAllRelationshipsAreExploded()
        {
            // Arrange
            var relationships = new List<StructurizrJsonRelationship>
            {
                new StructurizrJsonRelationship
                {
                    Id = "1",
                    SourceId = "1",
                    DestinationId = "2"
                },
                new StructurizrJsonRelationship
                {
                    Id = "2",
                    SourceId = "2",
                    DestinationId = "3"
                }
            };
            var exploder = new ModelExploder();

            // Act
            var result = exploder.ExplodeRelationships(relationships);

            // Assert
            Assert.That(result.Count, Is.EqualTo(2));
            Assert.That(result.First().StructurizrId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrSourceId, Is.EqualTo("1"));
            Assert.That(result.First().StructurizrDestinationId, Is.EqualTo("2"));
            Assert.That(result.Last().StructurizrId, Is.EqualTo("2"));
            Assert.That(result.Last().StructurizrSourceId, Is.EqualTo("2"));
            Assert.That(result.Last().StructurizrDestinationId, Is.EqualTo("3"));
            
        }
    }
}
