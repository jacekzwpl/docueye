using DocuEye.Infrastructure.MongoDB;
using DocuEye.WorkspaceImporter.Model;

namespace DocuEye.WorkspaceImporter.Persistence
{
    /// <summary>
    /// MongoDB context for WorkspaceImporter module
    /// </summary>
    public interface IWorkspaceImporterDBContext
    {
        /// <summary>
        /// Collection of Workspace Imports
        /// </summary>
        IGenericCollection<WorkspaceImport> WorkspaceImports { get; }
    }
}
