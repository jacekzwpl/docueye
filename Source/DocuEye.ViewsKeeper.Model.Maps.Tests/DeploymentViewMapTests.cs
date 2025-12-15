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
    public class DeploymentViewMapTests
    {
        [Test]
        public void Mapping_DeploymentView_To_WorkspaceView_Works()
        {
            var source = new DeploymentView
            {
                Id = "deploy-123",
                ViewType = "Deployment",
                Title = "Deployment View Title",
                Description = "A description of the deployment view",
                Key = "deploy-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<DeploymentView, object?>>
                {
                    {
                        nameof(WorkspaceView.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }

        [Test]
        public void Mapping_DeploymentView_To_ViewWithElement_Works()
        {
            var source = new DeploymentView
            {
                Id = "deploy-123",
                ViewType = "Deployment",
                Title = "Deployment View Title",
                Description = "A description of the deployment view",
                Key = "deploy-view-key"
            };
            var result = source.MapToWorkspaceView();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>(),
                customSourceResolvers: new Dictionary<string, Func<DeploymentView, object?>>
                {
                    {
                        nameof(ViewWithElement.Name), src => string.Format("[{0}]{1}", src.ViewType, src.Title ?? src.Description ?? src.Key)
                    }
                });
        }
    }
}
