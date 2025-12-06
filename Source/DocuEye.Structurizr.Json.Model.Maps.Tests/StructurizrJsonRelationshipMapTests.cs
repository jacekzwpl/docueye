using DocuEye.WorkspaceImporter.Api.Model.Relationships;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonRelationshipMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonRelationship_To_RelationshipToImport_Is_Correct()
        {
            // Arrange
            var relationship = new StructurizrJsonRelationship()
            {
                Id = "id",
                SourceId = "sourceId",
                DestinationId = "destinationId",
                Description = "description",
                Technology = "technology",
                InteractionStyle = "Synchronous",
                Tags = "tag1,tag2",
                LinkedRelationshipId = "linkedRelationshipId",
                Url = "http://example.com",
                Properties = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                }
            };
            // Act
            var relationshipToImport = relationship.ConvertToApiModel();
            // Assert
            MappingAssert.AssertMapped(
                relationship, relationshipToImport,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonRelationship, object?>>
                {
                    [nameof(RelationshipToImport.StructurizrId)] = s => s.Id,
                    [nameof(RelationshipToImport.StructurizrSourceId)] = s => s.SourceId,
                    [nameof(RelationshipToImport.StructurizrDestinationId)] = s => s.DestinationId,
                    [nameof(RelationshipToImport.StructurizrLinkedRelationshipId)] = s => s.LinkedRelationshipId,
                    [nameof(RelationshipToImport.Tags)] = s => string.IsNullOrWhiteSpace(s.Tags) ? null : s.Tags.Split(",").ToArray()
                }
            );
        }
    }
}
