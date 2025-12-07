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
    public class ImageToImportMapTests
    {
        [Test]
        public void Mapping_ImageToImport_To_Image_Works()
        {
            var source = new ImageToImport { 
                Name = "n",  
                Content = "base64", 
                Type = "png",
                DocumentationId = "docId"
            };
            var result = source.ToImage();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(Image.WorkspaceId),
                    nameof(Image.Id),
                }
            );
        }
    }
}
