using DocuEye.Infrastructure.Persistence.Model;
using System;

namespace DocuEye.WorkspaceImporter.Model
{
    /// <summary>
    /// Workspace Import
    /// </summary>
    public class WorkspaceImport : BaseEntity
    {
        /// <summary>
        /// Import Key should be unique for specific workspace. Can be for example commit hash for source
        /// </summary>
        public string Key { get; set; } = null!;
        /// <summary>
        /// Link to source of import can be for example link to source version or pipeline run that imported workspace
        /// </summary>
        public string? SourceLink { get; set; }
        /// <summary>
        /// Workspace Id for witch this import has been created
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Import start time
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// Import end time
        /// </summary>
        public DateTime? EndTime { get; set; }
    }
}
