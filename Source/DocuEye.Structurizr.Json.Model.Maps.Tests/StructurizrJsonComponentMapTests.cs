using DocuEye.Infrastructure.Tests.Common;
using DocuEye.Linter.Model;
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

        [Test]
        public void Mapping_StructurizrJsonComponent_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonComponent
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
                    nameof(LinterModelElement.InstanceOfIdentifier),
                    nameof(LinterModelElement.ParentIdentifier),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonComponent, object?>>
                {
                    { nameof(LinterModelElement.Identifier), s => s.DslId },
                    { nameof(LinterModelElement.Tags), s => string.IsNullOrWhiteSpace(s.Tags) ? new List<string>() : s.Tags.Split(',').Select(t => t.Trim()).ToList() }
                }
            );
            Assert.That(result.ParentIdentifier, Is.EqualTo("parent-id"));
        }
    }
}
