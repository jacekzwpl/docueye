using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonRelationshipViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonRelationshipView_To_RelationshipInViewToImport_Is_Correct()
        {
            // Arrange
            var relationshipView = new StructurizrJsonRelationshipView()
            {
                Id = "relationship-view-id",
                Description = "This is a relationship view description.",
                Response = true,
                Order = "1",
                Position = 5,
                Routing = "Direct"

            };
            // Act
            var element = relationshipView.ToRelationshipInViewToImport();
            // Assert
            MappingAssert.AssertMapped(
                relationshipView, element,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonRelationshipView, object?>> {
                    {
                        nameof(RelationshipInViewToImport.StructurizrId),
                        src => src.Id
                    }
                }
            );
        }
    }
}
