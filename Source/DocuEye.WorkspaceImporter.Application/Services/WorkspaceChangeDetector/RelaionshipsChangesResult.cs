using DocuEye.ModelKeeper.Model;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Services.WorkspaceChangeDetector
{
    /// <summary>
    /// Result of detecting relationships changes 
    /// </summary>
    public class RelaionshipsChangesResult
    {
        /// <summary>
        /// Relationships that should be updated
        /// </summary>
        public List<Relationship> RelationshipsToChange { get; set; } = new List<Relationship>();
        /// <summary>
        /// Relationships that should be created
        /// </summary>
        public List<Relationship> RelationshipsToAdd { get; set; } = new List<Relationship>();
        /// <summary>
        /// Relationships that should be removed form the model
        /// </summary>
        public List<Relationship> RelationshipsToDelete { get; set; } = new List<Relationship>();
    }
}
