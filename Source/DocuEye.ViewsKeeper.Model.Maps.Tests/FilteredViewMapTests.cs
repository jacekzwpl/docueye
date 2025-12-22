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
    public class FilteredViewMapTests
    {
        [Test]
        public void Mapping_FilteredView_To_WorkspaceView_Works()
        {
            var source = new FilteredView
            {
                Id = "filter-123",
                ViewType = "Filtered",
                Title = "Filtered View Title",
                Description = "A description of the filtered view",
                Key = "filter-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<FilteredView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_FilteredView_To_ViewWithElement_Works()
        {
            var source = new FilteredView
            {
                Id = "filter-123",
                ViewType = "Filtered",
                Title = "Filtered View Title",
                Description = "A description of the filtered view",
                Key = "filter-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<FilteredView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_FilteredView_To_FilteredViewDiagram_Works()
        {
            var source = new FilteredView
            {
                Id = "filter-123",
                ViewType = "Filtered",
                Title = "Filtered View Title",
                Description = "A description of the filtered view",
                Key = "filter-view-key",
                Elements = new[]
                {
                    new ElementView { Id = "elem-1", Type = "Class" },
                    new ElementView { Id = "elem-2", Type = "Interface" }
                },
                Relationships = new[]
                {
                    new RelationshipView { Id = "rel-1" }
                },
                AutomaticLayout = new AutomaticLayout(),
                WorkspaceId = "workspace-456",
                BaseViewKey = "base-view-key",
                Mode = "SomeMode",
                PaperSize = "A4",
                Tags = new[] { "tag1", "tag2" }

            };
            var result = source.MapToFilteredViewDiagram();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] { nameof(FilteredViewDiagram.LayoutData) });

            Assert.That(result.AutomaticLayout, Is.Not.Null);
            Assert.That(result.Relationships.Count(), Is.EqualTo(source.Relationships.Count()));
            Assert.That(result.Elements.Count(), Is.EqualTo(source.Elements.Count()));
        }
    }
}
