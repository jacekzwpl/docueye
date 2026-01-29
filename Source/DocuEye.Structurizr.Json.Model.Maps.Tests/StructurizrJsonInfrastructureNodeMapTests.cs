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
    public class StructurizrJsonInfrastructureNodeMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonInfrastructureNode_To_ElementToImport_Is_Correct()
        {
            // Arrange
            var infrastructureNode = new StructurizrJsonInfrastructureNode()
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
                Technology = "technology",
                Environment = "environment",
                
            };
            // Act
            var element = infrastructureNode.ConvertToApModel();
            // Assert
            MappingAssert.AssertMapped(
                infrastructureNode, element,
                ignoreDestProps: new[]
                {
                    nameof(ElementToImport.StructurizrParentId),
                    nameof(ElementToImport.StructurizrInstanceOfId),
                    nameof(ElementToImport.Location),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonInfrastructureNode, object?>>
                {
                    [nameof(ElementToImport.StructurizrId)] = s => s.Id,
                    [nameof(ElementToImport.Type)] = s => ElementType.InfrastructureNode,
                    [nameof(ElementToImport.Tags)] = s => string.IsNullOrWhiteSpace(s.Tags) ? null : s.Tags.Split(",").ToArray()
                }
            );
        }

        [Test]
        public void Mapping_StructurizrJsonInfrastructureNode_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonInfrastructureNode
            {
                Id = "Element-ID",
                Group = "Element Group",
                Url = "http://person.url",
                Name = "Element Name",
                Tags = "Tag1,Tag2",
                Description = "Element Description",
                Properties = new Dictionary<string, string>
                {
                    { DslPropertyNames.DslIdProperty, "MyIdentyfier" },
                    { "Key2", "Value2" }
                },
                Technology = "Element Technology"
            };
            // Act
            var result = source.ToLinterModelElement("parent-id");
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(LinterModelElement.InstanceOfIdentifier)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonInfrastructureNode, object?>>
                {
                    { nameof(LinterModelElement.Identifier), s => s.DslId },
                    { nameof(LinterModelElement.JsonModelId), s => s.Id  },
                    { nameof(LinterModelElement.Tags), s => string.IsNullOrWhiteSpace(s.Tags) ? new List<string>() : s.Tags.Split(',').Select(t => t.Trim()).ToList() },
                    { nameof(LinterModelElement.ParentIdentifier), s => "parent-id" }
                }
            );
        }
    }
}
