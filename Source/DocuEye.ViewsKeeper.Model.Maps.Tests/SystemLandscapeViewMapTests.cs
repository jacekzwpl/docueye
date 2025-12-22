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
    public class SystemLandscapeViewMapTests
    {
        [Test]
        public void Mapping_SystemLandscapeView_To_WorkspaceView_Works()
        {
            var source = new SystemLandscapeView
            {
                Id = "landscape-123",
                ViewType = "SystemLandscape",
                Title = "System Landscape View Title",
                Description = "A description of the system landscape view",
                Key = "system-landscape-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<SystemLandscapeView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }
        [Test]
        public void Mapping_SystemLandscapeView_To_ViewWithElement_Works()
        {
            var source = new SystemLandscapeView
            {
                Id = "landscape-123",
                ViewType = "SystemLandscape",
                Title = "System Landscape View Title",
                Description = "A description of the system landscape view",
                Key = "system-landscape-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<SystemLandscapeView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_SystemLandscapeView_To_SystemLandscapeViewDiagram_Works()
        {
            var source = new SystemLandscapeView
            {
                Id = "landscape-123",
                ViewType = "SystemLandscape",
                Title = "System Landscape View Title",
                Description = "A description of the system landscape view",
                Key = "system-landscape-view-key",
                AutomaticLayout = new AutomaticLayout(),
                Relationships = new[]
                {
                    new RelationshipView { Id = "rel-1" },
                    new RelationshipView { Id = "rel-2" }
                },
                Elements = new[]
                {
                    new ElementView { Id = "elem-1" },
                    new ElementView { Id = "elem-2" },
                    new ElementView { Id = "elem-3" }
                },
                EnterpriseBoundaryVisible = true,
                WorkspaceId = "workspace-456",
                PaperSize = "A4"
            };
            var result = source.MapToSystemLandscapeViewDiagram();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { nameof(SystemLandscapeViewDiagram.LayoutData) });

            Assert.That(result.AutomaticLayout, Is.Not.Null);
            Assert.That(result.Relationships.Count(), Is.EqualTo(source.Relationships.Count()));
            Assert.That(result.Elements.Count(), Is.EqualTo(source.Elements.Count()));
        }
    }
}
