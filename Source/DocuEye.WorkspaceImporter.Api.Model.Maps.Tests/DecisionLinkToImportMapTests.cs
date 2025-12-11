using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Docs;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class DecisionLinkToImportMapTests
    {
        [Test]
        public void Mapping_DecisionLinkToImport_To_DecisionLink_Works()
        {
            var source = new DecisionLinkToImport { 
                Description = "Description",
                StructurizrId = "StructurizrId"
            };
            var result = source.MapToDecisionLink();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(DecisionLink.Id),
                    nameof(DecisionLink.Title)
                }
                );
        }
    }
}
