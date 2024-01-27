using DocuEye.DocsKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.DocsKeeper.Application.Commads.SaveImages
{
    /// <summary>
    /// Save images command
    /// </summary>
    public class SaveImagesCommand : IRequest
    {
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Images that should exists in workspace after save
        /// </summary>
        public IEnumerable<Image> ImagesToAdd { get; set; } = Enumerable.Empty<Image>();
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        public SaveImagesCommand(string workspaceId)
        {
            this.WorkspaceId = workspaceId;
        }
    }
}
