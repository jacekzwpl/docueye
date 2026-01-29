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
    public class StructurizrJsonContainerInstanceMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonContainerInstance_To_ElementToImport_Is_Correct()
        {
            // Arrange
            var containerInstance = new StructurizrJsonContainerInstance()
            {
                Id = "id",
                ContainerId = "containerId",
                InstanceId = 1,
                Environment = "environment",
                Tags = "tag1,tag2",
                Properties = new Dictionary<string, string>
                {
                    ["key1"] = "value1",
                    ["key2"] = "value2"
                }
            };
            // Act
            var element = containerInstance.ConvertToApModel();
            // Assert
            MappingAssert.AssertMapped(
                containerInstance, element,
                ignoreDestProps: new[]
                {
                    nameof(ElementToImport.StructurizrParentId),
                    nameof(ElementToImport.Name),
                    nameof(ElementToImport.Url),
                    nameof(ElementToImport.Group),
                    nameof(ElementToImport.Location),
                    nameof(ElementToImport.Technology),
                    nameof(ElementToImport.Description),
                    nameof(ElementToImport.SourceCodeUrl),
                    nameof(ElementToImport.OwnerTeam)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonContainerInstance, object?>>
                {
                    [nameof(ElementToImport.StructurizrId)] = s => s.Id,
                    [nameof(ElementToImport.StructurizrInstanceOfId)] = s => s.ContainerId,
                    [nameof(ElementToImport.Type)] = s => ElementType.ContainerInstance,
                    [nameof(ElementToImport.Tags)] = s => string.IsNullOrWhiteSpace(s.Tags) ? null : s.Tags.Split(",").ToArray()
                }
            );
        }

        [Test]
        public void Mapping_StructurizrJsonContainerInstance_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonContainerInstance
            {
                Id = "Element-ID",
                ContainerId = "Container-ID",
                InstanceId = 42,
                Environment = "Production",
                Tags = "Tag1,Tag2",
                Properties = new Dictionary<string, string>
                {
                    { DslPropertyNames.DslIdProperty, "MyIdentyfier" },
                    { "Key2", "Value2" }
                },
            };
            // Act
            var result = source.ToLinterModelElement("parent-id", new LinterModelElement()
            {
                Identifier = "ContainerDslId",
                Name = "Element Name",
                Description = "Element Description",
                Technology = "Element Technology",
            });
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonContainerInstance, object?>>
                {
                    { nameof(LinterModelElement.Identifier), s => s.DslId },
                    { nameof(LinterModelElement.JsonModelId), s => s.Id  },
                    { nameof(LinterModelElement.Tags), s => string.IsNullOrWhiteSpace(s.Tags) ? new List<string>() : s.Tags.Split(',').Select(t => t.Trim()).ToList() },
                    { nameof(LinterModelElement.ParentIdentifier), s => "parent-id" },
                    { nameof(LinterModelElement.Name), s => "Element Name" },
                    { nameof(LinterModelElement.Description), s => "Element Description" },
                    { nameof(LinterModelElement.InstanceOfIdentifier), s => "ContainerDslId" },
                    { nameof(LinterModelElement.Technology), s => "Element Technology" },
                }
            );
        }
    }
}
