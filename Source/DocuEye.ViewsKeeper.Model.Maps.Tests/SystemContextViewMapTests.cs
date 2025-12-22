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
    public class SystemContextViewMapTests
    {
        [Test]
        public void Mapping_SystemContextView_To_WorkspaceView_Works()
        {
            var source = new SystemContextView
            {
                Id = "system-123",
                ViewType = "SystemContext",
                Title = "System Context View Title",
                Description = "A description of the system context view",
                Key = "system-context-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<SystemContextView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_SystemContextView_To_ViewWithElement_Works()
        {
            var source = new SystemContextView
            {
                Id = "system-123",
                ViewType = "SystemContext",
                Title = "System Context View Title",
                Description = "A description of the system context view",
                Key = "system-context-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<SystemContextView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_SystemContextView_To_SystemContextViewDiagram_Works()
        {
            var source = new SystemContextView
            {
                Id = "system-123",
                ViewType = "SystemContext",
                Title = "System Context View Title",
                Description = "A description of the system context view",
                Key = "system-context-view-key",
                SoftwareSystemId = "software-system-456",
                EnterpriseBoundaryVisible = true,
                AutomaticLayout = new AutomaticLayout(),
                Elements = new List<ElementView>()
                {
                    new ElementView { Id = "element-1", Type = "Person" },
                    new ElementView { Id = "element-2", Type = "SoftwareSystem" }
                },
                Relationships = new List<RelationshipView>()
                {
                    new RelationshipView { Id = "rel-1" }
                },
                PaperSize = "A4",
                WorkspaceId = "workspace-789"
            };
            var result = source.MapToSystemContextViewDiagram();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { nameof(SystemContextViewDiagram.LayoutData) });

            Assert.That(result.AutomaticLayout, Is.Not.Null);
            Assert.That(result.Relationships.Count(), Is.EqualTo(source.Relationships.Count()));
            Assert.That(result.Elements.Count(), Is.EqualTo(source.Elements.Count()));
        }
    }
}
