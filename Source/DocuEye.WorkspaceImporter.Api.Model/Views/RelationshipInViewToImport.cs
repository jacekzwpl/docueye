using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Views
{
    public class RelationshipInViewToImport
    {
        /// <summary>
        /// The ID of relationship in structurizr json
        /// </summary>
        public string StructurizrId { get; set; } = null!;
        /// <summary>
        /// The description of this relationship (used in dynamic views only).
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Signifies whether this relationship represents a return/response message (used in dynamic views only).
        /// </summary>
        public bool? Response { get; set; }
        /// <summary>
        /// Gets the order of this relationship (used in dynamic views only; e.g. 1.0, 1.1, 2.0, etc).
        /// </summary>
        public string? Order { get; set; }
    }
}
