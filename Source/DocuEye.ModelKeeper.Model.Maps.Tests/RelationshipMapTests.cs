using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.ModelKeeper.Model.Maps.Tests
{
    public class RelationshipMapTests
    {
        [Test]
        public void Mapping_Relationship_ToRelationshipView() {
            var source = new Relationship
            {
                Description = "description",
                DestinationId = "id-123",
                DestinationName = "name",
                DslId = "id-432",
                Id = "id-322",
                InteractionStyle = "style",
                LinkedRelationshipId = "id-765",
                Properties = new Dictionary<string,string>()
                {
                    { "description", "description" },
                },
                SourceId = "id-098",
                SourceName = "name",
                StructurizrId = "id-543",
                Tags = new List<string>() { "description" },
                Technology = "tech",
                Url = "url",
                WorkspaceId = "id-654"
            };

            var result = source.MapToRelationshipView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] {
                    nameof(RelationshipView.Order),
                    nameof(RelationshipView.Routing),
                    nameof(RelationshipView.Position),
                    nameof(RelationshipView.Response)
                });
        }
    }
}
