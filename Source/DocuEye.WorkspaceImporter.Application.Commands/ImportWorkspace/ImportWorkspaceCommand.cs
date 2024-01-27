using DocuEye.Structurizr.Model;
using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Commands.ImportWorkspace
{
    /// <summary>
    /// Import workspace command
    /// </summary>
    public class ImportWorkspaceCommand : IRequest<ImportWorkspaceResult>
    {
        /// <summary>
        /// Workspace Identifier
        /// </summary>
        public string? WorkspaceId { get; set; }
        /// <summary>
        /// Unique identifier of current import
        /// </summary>
        public string ImportKey { get; set; } = null!;
        public string? SourceLink { get; set; }
        /// <summary>
        /// Workspace data to import
        /// </summary>
        public StructurizrWorkspace WorkspaceData { get; set; } = null!;
    }
}
