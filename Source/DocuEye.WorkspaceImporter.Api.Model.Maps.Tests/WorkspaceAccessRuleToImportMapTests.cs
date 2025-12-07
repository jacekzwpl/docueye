using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.Workspace;
using DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class WorkspaceAccessRuleToImportMapTests
    {
        [Test]
        public void Mapping_WorkspaceAccessRuleToImport_To_WorkspaceAccessRule_Works()
        {
            var source = new WorkspaceAccessRuleToImport { 
                Role = "reader" };
            var result = source.ToWorkspaceAccessRule();
            MappingAssert.AssertMapped(
                source, result, ignoreDestProps: new[]
                {
                    nameof(WorkspaceAccessRule.Name)
                });
        }
    }
}
