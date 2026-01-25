using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Elements;
using NUnit.Framework.Internal;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonPersonMapTests
    {

        [Test]
        public void Mapping_StructurizrJsonPerson_To_ElementToImport_Is_Correct()
        {
            // Arrange
            var person = new StructurizrJsonPerson() {
                Id = "id",
                Name = "name",
                Url = "url",
                Description = "description",
                Group = "group",
                Location = "location",
                Properties = new Dictionary<string, string>() {
                    { DslPropertyNames.DslIdProperty, "dslId" },
                    { DslPropertyNames.OwnerTeam, "ownerTeam" },
                    { DslPropertyNames.SourceCodeUrl, "sourceCodeUrl" }
                },
                Tags = "tag1,tag2"
            };
            // Act
            var element = person.ConvertToApModel();

            // Assert
            MappingAssert.AssertMapped(
                person, element,
                ignoreDestProps: new[] {
                    nameof(ElementToImport.StructurizrParentId),
                    nameof(ElementToImport.StructurizrInstanceOfId),
                    nameof(ElementToImport.Technology),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonPerson, object?>>
                {
                    [nameof(ElementToImport.StructurizrId)] = s => s.Id,
                    [nameof(ElementToImport.Type)] = s => ElementType.Person,
                    [nameof(ElementToImport.Tags)] = s => string.IsNullOrWhiteSpace(s.Tags) ? null : s.Tags.Split(",").ToArray()
                }
            );
        }
   

        [Test]
        public void Mapping_StructurizrJsonPerson_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonPerson
            {
                Id = "PersonId",
                Group = "Person Group",
                Location = "Person Location",
                Url = "http://person.url",
                Name = "Person Name",
                Tags = "Tag1,Tag2",
                Description = "Person Description",
                Properties = new Dictionary<string, string>
                {
                    { "Key1", "Value1" },
                    { "Key2", "Value2" }
                },
            };
            // Act
            var result = source.ToLinterModelElement();
            // Assert
            MappingAssert.AssertMapped(
                source, result
            );
        } 
    }
}