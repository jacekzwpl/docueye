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
    }
}
