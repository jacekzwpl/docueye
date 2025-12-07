using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ViewsKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonDeploymentViewMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonDeploymentView_To_ViewToImport_Is_Correct()
        {
            // Arrange
            var source = new StructurizrJsonDeploymentView
            {
                Key = "deployment-view-1",
                Title = "Deployment View 1",
                SoftwareSystemId = "software-system-1",
                Description = "A description of the deployment view.",
                PaperSize = "A1_Landscape",
                AutomaticLayout = new StructurizrJsonAutomaticLayout
                {
                    Implementation = "implementation",
                    RankDirection = "LeftRight",
                    RankSeparation = 350,
                    NodeSeparation = 175,
                    EdgeSeparation = 35,
                    Vertices = false
                },
                Elements = new List<StructurizrJsonElementView>
                {
                    new StructurizrJsonElementView { Id = "element-1", X = 400, Y = 500 },
                    new StructurizrJsonElementView { Id = "element-2", X = 600, Y = 700 }
                },
                Relationships = new List<StructurizrJsonRelationshipView>
                {
                    new StructurizrJsonRelationshipView { Id = "relationship-1", Routing = "Orthogonal" }
                }
            };
            // Act
            var result = source.ToViewToImport();
            // Assert
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[]
                {
                    nameof(ViewToImport.Content),
                    nameof(ViewToImport.Relationships),
                    nameof(ViewToImport.Elements),
                    nameof(ViewToImport.AutomaticLayout),
                    nameof(ViewToImport.ContentType),
                    nameof(ViewToImport.BaseViewKey),
                    nameof(ViewToImport.Mode),
                    nameof(ViewToImport.Tags),
                    nameof(ViewToImport.ExternalBoundariesVisible),
                },
                customSourceResolvers: new Dictionary<string, Func<StructurizrJsonDeploymentView, object?>>
                {
                    { nameof(ViewToImport.ViewType), s => ViewType.DeploymentView },
                    { nameof(ViewToImport.StructurizrElementId), s => s.SoftwareSystemId },
                }
            );
        }
    }
}
