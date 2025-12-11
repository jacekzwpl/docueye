using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ModelKeeper.Application.Model;
using DocuEye.ViewsKeeper.Model;

namespace DocuEye.ModelKeeper.Model.Maps.Tests
{
    public class ElementMapTests
    {
        [Test]
        public void Mapping_Element_To_ElementView_Works()
        {
            var source = new Element
            {
                Tags = new List<string>()
                {
                    "tag1", "tag2"
                },
                SourceCodeUrl = "url",
                StructurizrId = "id-456",
                Technology = "tech",
                Description = "description",
                DslId = "id-567",
                Group = "group",
                Id = "id-123",
                InstanceOfId = "id-321",
                Location = "location",
                Name = "name",
                OwnerTeam = "teamname",
                ParentId = "parent",
                Properties = new Dictionary<string, string>()
                {
                    {"prop","value" }
                },
                Type = "type",
                Url = "url",
                WorkspaceId = "id"
            };

            var result = source.MapToElementView();

            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] {
                    nameof(ElementView.X),
                    nameof(ElementView.Y),
                    nameof(ElementView.DiagramId),
                });
        }

        [Test]
        public void Mapping_Element_To_WorkspaceCatalogElement_Works()
        {
            var source = new Element
            {
                Id = "id-123",
                Name = "name",
                Type = "type",
                Description = "description",
            };
            var result = source.MapToWorkspaceCatalogElement();
            MappingAssert.AssertMapped(
                source, result);
        }

        [Test]
        public void Mapping_Element_To_ChildElement_Works()
        {
            var source = new Element
            {
                Id = "id-123",
                Name = "name",
                Type = "type",
                Description = "description",
                InstanceOfId = "id-321",
            };
            var result = source.MapToChildElement();
            MappingAssert.AssertMapped(
                source, result);
        }

        [Test]
        public void Mapping_Element_To_ElementDependence_Works()
        {
            var source = new Element
            {
                Id = "id-123",
                Name = "name",
                Type = "type",
                Description = "description",
            };
            var result = source.MapToElementDependence();
            MappingAssert.AssertMapped(
                source, result, 
                ignoreDestProps: new[] { 
                    nameof(ElementDependence.RelationDescription),
                    nameof(ElementDependence.RelationTechnology)
                }  
            );
        }

        [Test]
        public void Mapping_Element_To_ElementConsumer_Works()
        {
            var source = new Element
            {
                Id = "id-123",
                Name = "name",
                Type = "type",
                Description = "description",
            };
            var result = source.MapToElementConsumer();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] {
                    nameof(ElementConsumer.RelationDescription),
                    nameof(ElementConsumer.RelationTechnology)
                }
            );
        }
    }
}
