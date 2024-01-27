using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Events
{
    /// <summary>
    /// Event that is fired when relationship should be removed according to detected changes during import
    /// </summary>
    public class RelationshipDeletedEvent : INotification
    {
        /// <summary>
        /// The ID of workspace that relationship belongs to
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// The ID of the relationship
        /// </summary>
        public string RelationshipId { get; set; }
        /// <summary>
        /// The ID of the import during whitch relationship was removed
        /// </summary>
        public string ImportId { get; set; }
        /// <summary>
        /// The key of the import during whitch relationship was removed
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// The source link of the import during whitch relationship was removed
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
        /// <param name="workspaceId">The ID of workspace that relationship belongs to</param>
        /// <param name="relationshipId">The ID of the relationship</param>
        /// <param name="importId">The ID of the import during whitch relationship was removed</param>
        /// <param name="importKey">The key of the import during whitch relationship was removed</param>
        /// <param name="importLink">The source link of the import during whitch relationship was removed</param>
        /// <param name="sourceElementName">Relationship source element name</param>
        /// <param name="destinationElementName">Relationship destination element name</param>
        public RelationshipDeletedEvent(string workspaceId, string relationshipId, string importId, string importKey, string? importLink, string sourceElementName, string destinationElementName)
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
