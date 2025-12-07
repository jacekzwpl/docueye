using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class DocumentationToImportMapTests
    {
        [Test]
        public void Mapping_DocumentationToImport_To_Documentation_Works()
        {
            var source = new DocumentationToImport
            {
                Sections = new[]
                {
                    new DocumentationSectionToImport { Content = "Content 1", Format = "markdown" },
                    new DocumentationSectionToImport {  Content = "Content 2", Format = "html" }
                },
                Id = "doc-123",
                StructurizrElementId = "element-456"
            };
            var result = source.ToDocumentation();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(Documentation.Sections),
                    nameof(Documentation.WorkspaceId),
                    nameof(Documentation.ElementId)
                }
            );

            Assert.That(result.Sections.Count(), Is.EqualTo(2));
        }
    }
}
