using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonSoftwareSystemMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonSoftwareSystem_To_ElementToImport_Is_Correct()
        {
            // Arrange
            var softwareSystem = new StructurizrJsonSoftwareSystem()
            {
                Id = "id",
                Name = "name",
                Url = "url",
                Description = "description",
                Group = "group",
                Properties = new Dictionary<string, string>() {
                    { DslPropertyNames.DslIdProperty, "dslId" },
                    { DslPropertyNames.OwnerTeam, "ownerTeam" },
                    { DslPropertyNames.SourceCodeUrl, "sourceCodeUrl" }
                },
                Tags = "tag1,tag2",
                Location = "LocationValue"
            };
            // Act
            var element = softwareSystem.ConvertToApModel();
            // Assert
            MappingAssert.AssertMapped(
                softwareSystem, element,
                ignoreDestProps: new[]
                {
                    nameof(ElementToImport.StructurizrParentId),
                    nameof(ElementToImport.StructurizrInstanceOfId),
                    nameof(ElementToImport.Technology),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonSoftwareSystem, object?>>
                {
                    [nameof(ElementToImport.StructurizrId)] = s => s.Id,
                    [nameof(ElementToImport.Type)] = s => ElementType.SoftwareSystem,
                    [nameof(ElementToImport.Tags)] = s => string.IsNullOrWhiteSpace(s.Tags) ? null : s.Tags.Split(",").ToArray()
                }
            );
        }
    }
}
