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
    public class ContainerViewMapTests
    {
        [Test]
        public void Mapping_ContainerView_To_WorkspaceView_Works()
        {
            var source = new ContainerView
            {
                Id = "cont-123",
                ViewType = "Container",
                Title = "Container View Title",
                Description = "A description of the container view",
                Key = "cont-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<ContainerView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_ContainerView_To_ViewWithElement_Works() {
            var source = new ContainerView
            {
                Id = "cont-123",
                ViewType = "Container",
                Title = "Container View Title",
                Description = "A description of the container view",
                Key = "cont-view-key"
            };
            var result = source.MapToViewWithElement();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<ContainerView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_ContainerView_To_ContainerViewDiagram_Works()
        {
            var source = new ContainerView
            {
                Id = "cont-456",
                ViewType = "Container",
                Title = "Another Container View",
                Description = "Description of another container view",
                Key = "another-cont-view-key",
                SoftwareSystemId = "sys-789",
                ExternalSoftwareSystemBoundariesVisible = true,
                AutomaticLayout = new AutomaticLayout()
                {
                    Implementation = "dot",
                    RankDirection = "TB",
                    RankSeparation = 100,
                    NodeSeparation = 50,
                    EdgeSeparation = 10,
                    Vertices = true
                },
                Elements = new List<ElementView>()
                {
                    new ElementView() { Id = "elem-1", Type = "Container", Description = "First element" },
                    new ElementView() { Id = "elem-2", Type = "Container", Description = "Second element" }
                },
                Relationships = new List<RelationshipView>()
                {
                    new RelationshipView() { Id = "rel-1", Description = "First relationship" },
                    new RelationshipView() { Id = "rel-2", Description = "Second relationship" } 
                },
                PaperSize = "A4",
                WorkspaceId = "workspace-123"

            };
            var result = source.MapToContainerViewDiagram();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] {nameof(ContainerViewDiagram.LayoutData)});

            Assert.That(result.AutomaticLayout, Is.Not.Null);
            Assert.That(result.Relationships.Count(), Is.EqualTo(source.Relationships.Count()));
            Assert.That(result.Elements.Count(), Is.EqualTo(source.Elements.Count()));
        }
    }
}
