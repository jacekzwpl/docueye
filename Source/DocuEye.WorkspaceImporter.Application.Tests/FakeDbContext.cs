using DocuEye.Infrastructure.MongoDB;
using DocuEye.Infrastructure.Tests.FakeMongoDB;
using DocuEye.WorkspaceImporter.Model;
using DocuEye.WorkspaceImporter.Persistence;

namespace DocuEye.WorkspaceImporter.Application.Tests
{
    public class FakeDbContext : IWorkspaceImporterDBContext
    {

        private List<WorkspaceImport> workspaceImports { get; set; }

        public IGenericCollection<WorkspaceImport> WorkspaceImports
        {
            get
            {
                return new FakeGenericCollection<WorkspaceImport>(this.workspaceImports);
            }
        }

        public FakeDbContext(List<WorkspaceImport>? workspaceImports = null)
        {
            this.workspaceImports = workspaceImports ?? new List<WorkspaceImport>();
        }

       
    }
}
