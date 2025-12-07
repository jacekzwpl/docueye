using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonDocumentationSectionMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonDocumentationSection_To_DocumentationSectionToImport_Is_Correct()
        {
            // Arrange
            var section = new StructurizrJsonDocumentationSection()
            {
                Content = "content",
                Format = "format",
                Order = 1,
                FileName = "filename.md"
            };
            // Act
            var element = section.ConvertToApiModel();
            // Assert
            MappingAssert.AssertMapped(
                section, element 
            );
        }
    }
}
