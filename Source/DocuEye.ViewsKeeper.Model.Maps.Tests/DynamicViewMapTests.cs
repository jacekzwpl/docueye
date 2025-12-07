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
    public class DynamicViewMapTests
    {
        [Test]
        public void Mapping_DynamicView_To_WorkspaceView_Works()
        {
            var source = new DynamicView
            {
                Id = "dynamic-123",
                ViewType = "Dynamic",
                Title = "Dynamic View Title",
                Description = "A description of the dynamic view",
                Key = "dynamic-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<DynamicView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }
        [Test]
        public void Mapping_DynamicView_To_ViewWithElement_Works()
        {
            var source = new DynamicView
            {
                Id = "dynamic-123",
                ViewType = "Dynamic",
                Title = "Dynamic View Title",
                Description = "A description of the dynamic view",
                Key = "dynamic-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<DynamicView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }
    }
}
