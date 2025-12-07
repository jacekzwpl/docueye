using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Relationships;
using DocuEye.WorkspaceImporter.Api.Model.Maps;
using DocuEye.ModelKeeper.Model;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class RelationshipToImportMapTests
    {
        [Test]
        public void Mapping_RelationshipToImport_To_Relationship_Works()
        {
            var source = new RelationshipToImport
            {
                Description = "calls",
                Technology = "HTTP",
                Tags = new[] { "sync" },
                Url = "http://example.com/api",
                Properties = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                },
                StructurizrId = "rel-123",
                StructurizrSourceId = "src-456",
                StructurizrDestinationId = "dest-789",
                StructurizrLinkedRelationshipId = "linked-101112",
                InteractionStyle = "Synchronous",
                DslId = "dsl-131415"
            };

            var result = source.ToRelationship();

            MappingAssert.AssertMapped(
                source, result, 
                ignoreDestProps: new[] { 
                    nameof(Relationship.SourceId), 
                    nameof(Relationship.DestinationId),
                    nameof(Relationship.LinkedRelationshipId),
                    nameof(Relationship.WorkspaceId),
                    nameof(Relationship.SourceName),
                    nameof(Relationship.DestinationName),
                    nameof(Relationship.Id)
                });
        }
    }
}
