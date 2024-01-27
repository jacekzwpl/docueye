using DocuEye.DocsKeeper.Model;
using MediatR;
using System.Collections.Generic;
using System.Linq;

namespace DocuEye.DocsKeeper.Application.Commads.SaveDocumentationChanges
{
    /// <summary>
    /// Save documentation command
    /// </summary>
    public class SaveDocumentationCommand : IRequest
    {
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Documentations that should exists in workspace after save
        /// </summary>
        public IEnumerable<Documentation> DocumentationsToAdd { get; set; } = Enumerable.Empty<Documentation>();
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        public SaveDocumentationCommand(string workspaceId) {
            this.WorkspaceId = workspaceId;
        }
    }
}
