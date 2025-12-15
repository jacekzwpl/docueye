

using DocuEye.Infrastructure.Mediator.Commands;

namespace DocuEye.WorkspaceImporter.Application.Commands.FinalizeImport
{
    public class FinalizeImportCommand : ICommand<FinalizeImportResult>
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
