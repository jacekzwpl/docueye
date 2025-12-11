using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonComponentMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonComponent_To_ElementToImport_Is_Correct()
        {
            // Arrange
            var component = new StructurizrJsonComponent()
            {
                Id = "id",
                Name = "name",
                Url = "url",
                Description = "description",
                Group = "group",
                Technology = "technology",
                Properties = new Dictionary<string, string>() {
                    { DslPropertyNames.DslIdProperty, "dslId" },
                    { DslPropertyNames.OwnerTeam, "ownerTeam" },
                    { DslPropertyNames.SourceCodeUrl, "sourceCodeUrl" }
                },
                Tags = "tag1,tag2",
                
            };
            // Act
            var element = component.ConvertToApModel();
            // Assert
            MappingAssert.AssertMapped(
                component, element,
                ignoreDestProps: new[]
                {
                    nameof(ElementToImport.StructurizrParentId),
                    nameof(ElementToImport.StructurizrInstanceOfId),
                    nameof(ElementToImport.Location),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonComponent, object?>>
                {
                    [nameof(ElementToImport.StructurizrId)] = s => s.Id,
                    [nameof(ElementToImport.Type)] = s => ElementType.Component,
                    [nameof(ElementToImport.Tags)] = s => string.IsNullOrWhiteSpace(s.Tags) ? null : s.Tags.Split(",").ToArray()
                }
            );
        }
    }
}
