using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspacesKeeper.Application.Commands.SaveWorkspace
{
    /// <summary>
    /// Save workspace
    /// </summary>
    public class SaveWorkspaceCommand : IRequest
    {
        /// <summary>
        /// Workspace data
        /// </summary>
        public Workspace Workspace { get; set; } = null!;
    }
}
