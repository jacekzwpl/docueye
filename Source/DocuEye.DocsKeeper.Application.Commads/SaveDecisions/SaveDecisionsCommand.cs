using DocuEye.DocsKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.SaveDecisions
{
    /// <summary>
    /// Save decisions command
    /// </summary>
    public class SaveDecisionsCommand : IRequest
    {
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Decisions that should exists in workspace after save
        /// </summary>
        public IEnumerable<Decision> DecisionsToAdd { get; set; } = Enumerable.Empty<Decision>();
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        public SaveDecisionsCommand(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
