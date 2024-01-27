using DocuEye.Infrastructure.Persistence.Model;
using System;

namespace DocuEye.ChangeTracker.Model
{
    /// <summary>
    /// Represents Model change
    /// </summary>
    public class ModelChange : BaseEntity
    {
        /// <summary>
        /// Id of workspace for witch change was created
        /// </summary>
        public string WorkspaceId { get; set; } = null!;
        /// <summary>
        /// Id of element for witch change was created
        /// </summary>
        public string? ElementId { get; set; }
        /// <summary>
        /// Id of relationship for witch change was created
        /// </summary>
        public string? RelationshipId { get; set; }
        /// <summary>
        /// Id of import in witch change was created
        /// </summary>
        public string ImportId { get; set; } = null!;
        /// <summary>
        /// The key of import in witch change was created
        /// </summary>
        public string ImportKey { get; set; } = null!;
        /// <summary>
        /// Source link of import in witch change was created
        /// </summary>
        public string? ImportLink { get; set; }
        /// <summary>
        /// Time of change creation
        /// </summary>
        public DateTime EventTime { get; set; }
        /// <summary>
        /// Change type
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// Change description
        /// </summary>
        public string Description { get; set; } = null!;

    }
}
