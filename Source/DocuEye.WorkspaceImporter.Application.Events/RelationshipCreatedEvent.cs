

using DocuEye.Infrastructure.Mediator.Events;

namespace DocuEye.WorkspaceImporter.Application.Events
{
    /// <summary>
    /// Event that is fired when relationship should be created according to detected changes during import
    /// </summary>
    public class RelationshipCreatedEvent : IEvent
    {
        /// <summary>
        /// The ID of the workspace
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Relationship ID
        /// </summary>
        public string RelationshipId { get; set; }
        /// <summary>
        /// The ID of import during whitch relationship was created
        /// </summary>
        public string ImportId { get; set; }
        /// <summary>
        /// The key of import during whitch relationship was created
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// The source link of import during whitch relationship was created
        /// </summary>
        public string? ImportLink { get; set; }
        /// <summary>
        /// Relationship source element name
        /// </summary>
        public string SourceElementName { get; set; }
        /// <summary>
        /// Relationship destination element name
        /// </summary>
        public string DestinationElementName { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of the workspace</param>
        /// <param name="relationshipId">Relationship ID</param>
        /// <param name="importId">The ID of import during whitch relationship was created</param>
        /// <param name="importKey">The key of import during whitch relationship was created</param>
        /// <param name="importLink">The source link of import during whitch relationship was created</param>
        /// <param name="sourceElementName">Relationship source element name</param>
        /// <param name="destinationElementName">Relationship destination element name</param>
        public RelationshipCreatedEvent(string workspaceId, string relationshipId, string importId, string importKey, string? importLink, string sourceElementName, string destinationElementName)
        {
            WorkspaceId = workspaceId;
            RelationshipId = relationshipId;
            ImportId = importId;
            ImportKey = importKey;
            ImportLink = importLink;
            SourceElementName = sourceElementName;
            DestinationElementName = destinationElementName;
        }
    }
}
