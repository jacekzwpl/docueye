using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using DocuEye.WorkspaceImporter.Api.Model.Maps;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class ElementToImportMapTests
    {
        [Test]
        public void Mapping_ElementToImport_To_Element_Works()
        {
            var source = new ElementToImport
            {
                Name = "n",
                Description = "d",
                Technology = "t",
                Tags = new[] { "tag1", "tag2" },
                Type = "SoftwareSystem",
                Properties = new Dictionary<string, string>
                {
                    { "key1", "value1" },
                    { "key2", "value2" }
                },
                DslId = "dslId",
                StructurizrId = "structurizrId",
                StructurizrParentId = "structurizrParentId",
                StructurizrInstanceOfId = "structurizrInstanceOfId",
                Group = "group",
                Url = "http://example.com",
                Location = "Location",
                SourceCodeUrl = "http://sourcecode.com",
                OwnerTeam = "Team A"
            };

            var result = source.ToElement();

            MappingAssert.AssertMapped(
                source, result, 
                ignoreDestProps: new[] { 
                    nameof(Element.Id),
                    nameof(Element.ParentId),
                    nameof(Element.InstanceOfId),
                    nameof(Element.WorkspaceId)
                });
        }
    }
}
