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
    }
}
