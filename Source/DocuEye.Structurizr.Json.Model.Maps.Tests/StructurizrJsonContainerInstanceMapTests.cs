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
    }
}
