using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.WorkspaceImporter.Api.Model.Workspace;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocuEye.WorkspaceImporter.Application.Commands.StartImport
{
    public class StartImportCommand : ICommand<StartImportResult>
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

        public string Visibility { get; set; } = "public";

        public IEnumerable<WorkspaceAccessRuleToImport> AccessRules { get; set; } = Enumerable.Empty<WorkspaceAccessRuleToImport>();
    }
}
