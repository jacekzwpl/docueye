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
    public class DecisionToImportMapTests
    {
        [Test]
        public void Mapping_DecisionToImport_To_Decision_Works()
        {
            var source = new DecisionToImport { 
                StrucuturizrId = "id", 
                Title = "t", 
                Status = "Accepted", 
                Date = "2024-01-01", 
                Content = "c", 
                Format = "markdown",
                DocumentationId = "docId",
                StrucuturizrElementId = "elId",
                Links = new[]
                {
                    new DecisionLinkToImport
                    {
                        Description = "desc",
                        StructurizrId = "linkId"
                    }
                }
            };
            var result = source.MapToDecision();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { 
                    nameof(Decision.Links),
                    nameof(Decision.ElementId),
                    nameof(Decision.WorkspaceId),
                    nameof(Decision.ImportKey),
                    nameof(Decision.Id)
                },
                customSourceResolvers: new Dictionary<string, Func<DecisionToImport, object?>>
                {
                    { nameof(Decision.DslId), s => s.StrucuturizrId },
                    { nameof(Decision.Date), s => DateTime.Parse(s.Date!) }
                }
            );
        }
    }
}
