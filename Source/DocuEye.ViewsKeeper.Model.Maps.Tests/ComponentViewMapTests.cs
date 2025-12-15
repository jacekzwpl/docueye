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
    public class ComponentViewMapTests
    {
        [Test]
        public void Mapping_ComponentView_To_WorkspaceView_Works()
        {
            var source = new ComponentView
            {
                Id = "comp-123",
                ViewType = "Component",
                Title = "Component View Title",
                Description = "A description of the component view",
                Key = "comp-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<ComponentView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_ComponentView_To_ViewWithElement_Works()
        {
            var source = new ComponentView
            {
                Id = "comp-456",
                ViewType = "Component",
                Title = null,
                Description = null,
                Key = "comp-view-key-2"
            };
            var result = source.MapToViewWithElement();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<ComponentView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }
    }
}
