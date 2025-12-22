using DocuEye.Infrastructure.Tests.Common;
using DocuEye.ViewsKeeper.Api.Model;
using DocuEye.ViewsKeeper.Application.Model;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Model.Maps.Tests
{
    public class DeploymentViewMapTests
    {
        [Test]
        public void Mapping_DeploymentView_To_WorkspaceView_Works()
        {
            var source = new DeploymentView
            {
                Id = "deploy-123",
                ViewType = "Deployment",
                Title = "Deployment View Title",
                Description = "A description of the deployment view",
                Key = "deploy-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<DeploymentView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_DeploymentView_To_ViewWithElement_Works()
        {
            var source = new DeploymentView
            {
                Id = "deploy-123",
                ViewType = "Deployment",
                Title = "Deployment View Title",
                Description = "A description of the deployment view",
                Key = "deploy-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<DeploymentView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_DeploymentView_To_DeploymentViewDiagram_Works()
        {
            var source = new DeploymentView
            {
                Id = "deploy-789",
                ViewType = "Deployment",
                Title = "Deployment View Diagram Title",
                Description = "A description of the deployment view diagram",
                Key = "deploy-view-diagram-key",
                Elements = new[]
                {
                    new ElementView(),
                    new ElementView()
                },
                Relationships = new[]
                {
                    new RelationshipView()
                },
                AutomaticLayout = new AutomaticLayout()
                {
                    Implementation = "dot",
                    RankDirection = "TB",
                    RankSeparation = 100,
                    NodeSeparation = 50,
                    EdgeSeparation = 10,
                    Vertices = true
                },
                PaperSize = "A4",
                SoftwareSystemId = "system-456",
                WorkspaceId = "workspace-123"
            };
            var result = source.MapToDeploymentViewDiagram();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { nameof(DeploymentViewDiagram.LayoutData) });

            Assert.That(result.AutomaticLayout, Is.Not.Null);
            Assert.That(result.Relationships.Count(), Is.EqualTo(source.Relationships.Count()));
            Assert.That(result.Elements.Count(), Is.EqualTo(source.Elements.Count()));
        }
    }
}
