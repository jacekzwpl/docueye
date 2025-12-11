using DocuEye.Infrastructure.Tests.Common;
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
    }
}
