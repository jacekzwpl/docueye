using DocuEye.Infrastructure.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspacesKeeper.Model.Maps.Tests
{
    public class WorkspaceMapTests
    {
        [Test]
        public void Mapping_Workspace_To_FoundedWorkspace_Works()
        {
            var source = new Workspace
            {
                Id = "ws-123",
                Name = "Workspace Name",
                Description = "Workspace Description"
            };
            var result = source.MapToFoundedWorkspace();
            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: Array.Empty<string>());
        }
    }
}
