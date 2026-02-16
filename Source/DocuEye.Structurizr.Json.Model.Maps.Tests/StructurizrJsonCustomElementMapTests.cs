using DocuEye.Infrastructure.Tests.Common;
using DocuEye.Linter.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonCustomElementMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonCustomElement_To_LinterModelElement_Works()
        {
            // Arrange
            var source = new StructurizrJsonCustomElement
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
                Metadata = "Element Metadata"
            };
            // Act
            var result = source.ToLinterModelElement();
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(LinterModelElement.Technology),
                    nameof(LinterModelElement.InstanceOfIdentifier),
                    nameof(LinterModelElement.ParentIdentifier),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonCustomElement, object?>>
                {
                    { nameof(LinterModelElement.Identifier), s => s.DslId },
                    { nameof(LinterModelElement.JsonModelId), s => s.Id  },
                    { nameof(LinterModelElement.Tags), s => string.IsNullOrWhiteSpace(s.Tags) ? new List<string>() : s.Tags.Split(',').Select(t => t.Trim()).ToList() }
                }
            );

        }
    }
}
