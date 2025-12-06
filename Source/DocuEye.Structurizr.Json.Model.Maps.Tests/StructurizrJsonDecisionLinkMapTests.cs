using DocuEye.WorkspaceImporter.Api.Model.Docs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonDecisionLinkMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonDecisionLink_To_DecisionLinkToImport_Is_Correct()
        {
            // Arrange
            var decisionLink = new StructurizrJsonDecisionLink()
            {
                Id = "link-id",
                Description = "This is a description of the decision link."
            };
            // Act
            var element = decisionLink.ConvertToApiModel();
            // Assert
            MappingAssert.AssertMapped(
                decisionLink, element,
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonDecisionLink, object?>>()
                {
                    { nameof(DecisionLinkToImport.StructurizrId), src => src.Id }
                }
            );
        }
    }
}
