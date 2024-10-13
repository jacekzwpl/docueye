using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Commands.FinalizeImport
{
    public class FinalizeImportCommand : IRequest<FinalizeImportResult>
    {
        /// <summary>
        /// Workspace Identifier
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Unique identifier of current import
        /// </summary>
        public string ImportKey { get; set; } = null!;
    }
}
