using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace
{
    /// <summary>
    /// Save workspace
    /// </summary>
    public class SaveWorkspaceCommand : ICommand
    {
        /// <summary>
        /// Workspace data
        /// </summary>
        public Workspace Workspace { get; set; } = null!;
    }
}
