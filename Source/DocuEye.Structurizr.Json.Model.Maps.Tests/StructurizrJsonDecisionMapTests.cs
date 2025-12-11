using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Docs;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonDecisionMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonDecision_To_DecisionToImport_Is_Correct()
        {
            // Arrange
            var decision = new StructurizrJsonDecision()
            {
                Id = "decision-id",
                ElementId = "element-id",
                Title = "Decision Title",
                Status = "Approved",
                Date = "date",
                Content = "This is the content of the decision.",
                Format = "markdown"
            };
            // Act
            var element = decision.ConvertToApiModel();
            // Assert
            MappingAssert.AssertMapped(
                decision, element,
                ignoreDestProps: new[] { 
                    nameof(DecisionToImport.DocumentationId),
                    nameof(DecisionToImport.Links)
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonDecision, object?>> {
                    {
                        nameof(DecisionToImport.StrucuturizrId),
                        src => src.Id
                    },          
                    {
                        nameof(DecisionToImport.StrucuturizrElementId),
                        src => src.ElementId 
                    }
                }
            );
        }
    }
}
