using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.StartImport
{
    public class StartImportCommand : IRequest<StartImportResult>
    {
        /// <summary>
        /// Workspace Identifier
        /// </summary>
        public string? WorkspaceId { get; set; }
        /// <summary>
        /// Unique identifier of current import
        /// </summary>
        public string ImportKey { get; set; } = null!;
        /// <summary>
        /// Link to source version form which worskpace is imported
        /// </summary>
        public string? SourceLink { get; set; }
        /// <summary>
        /// Workspace name
        /// </summary>
        public string WorkspaceName { get; set; } = null!;
        /// <summary>
        /// Workspace description
        /// </summary>
        public string? WorkspaceDescription { get; set; }
    }
}
