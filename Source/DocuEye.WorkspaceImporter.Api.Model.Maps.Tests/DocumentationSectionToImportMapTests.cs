using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class DocumentationSectionToImportMapTests
    {
        [Test]
        public void Mapping_DocumentationSectionToImport_To_DocumentationSection_Works()
        {
            var source = new DocumentationSectionToImport { 
                Content = "Some content",
                Format = "markdown",
                Order = 1
            };
            var result = source.MapToDocumentationSection();
            MappingAssert.AssertMapped(source, result);
        }
    }
}
