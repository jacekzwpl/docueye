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
    }
}
