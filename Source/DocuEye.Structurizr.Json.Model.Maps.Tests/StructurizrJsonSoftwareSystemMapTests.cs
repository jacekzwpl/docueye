using DocuEye.Infrastructure.Tests.Common;
using DocuEye.Linter.Model;
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

        [Test]
        public void Mapping_StructurizrJsonSoftwareSystem_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonSoftwareSystem
            {
                Id = "Element-ID",
                Group = "Element Group",
                Location = "Element Location",
                Url = "http://person.url",
                Name = "Element Name",
                Tags = "Tag1,Tag2",
                Description = "Element Description",
                Properties = new Dictionary<string, string>
                {
                    { DslPropertyNames.DslIdProperty, "MyIdentyfier" },
                    { "Key2", "Value2" }
                },
            };
            // Act
            var result = source.ToLinterModelElement();
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(LinterModelElement.InstanceOfIdentifier),
                    nameof(LinterModelElement.ParentIdentifier),
                    nameof(LinterModelElement.Technology),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonSoftwareSystem, object?>>
                {
                    { nameof(LinterModelElement.Identifier), s => s.DslId },
                    { nameof(LinterModelElement.Tags), s => string.IsNullOrWhiteSpace(s.Tags) ? new List<string>() : s.Tags.Split(',').Select(t => t.Trim()).ToList() }
                }
            );
        }
    }
}
