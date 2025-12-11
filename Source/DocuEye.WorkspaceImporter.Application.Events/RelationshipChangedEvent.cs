
using DocuEye.Infrastructure.Mediator.Events;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Events
{
    /// <summary>
    /// Event that is fired when relationship changes was detected during import
    /// </summary>
    public class RelationshipChangedEvent : IEvent
    {
        /// <summary>
        /// The ID of workspace in whitch relationship exists
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// The ID of relationship that was updated
        /// </summary>
        public string RelationshipId { get; set; }
        /// <summary>
        /// The ID of import during whitch relationship was updated
        /// </summary>
        public string ImportId { get; set; }
        /// <summary>
        /// The key of import during whitch relationship was updated
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// The source link of import during whitch relationship was updated
        /// </summary>
        public string? ImportLink { get; set; }
        /// <summary>
        /// Source element name of the relationship
        /// </summary>
        public string SourceElementName { get; set; }
        /// <summary>
        /// Destination element name of the relationship
        /// </summary>
        public string DestinationElementName { get; set; }
        /// <summary>
        /// Relationship changes details
        /// </summary>
        public IDictionary<string, Tuple<string, string>> Changes { get; set; } = new Dictionary<string, Tuple<string, string>>();
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of workspace in whitch relationship exists</param>
        /// <param name="relationshipId">The ID of relationship that was updated</param>
        /// <param name="importId">The ID of import during whitch relationship was updated</param>
        /// <param name="importKey">The key of import during whitch relationship was updated</param>
        /// <param name="importLink">The source link of import during whitch relationship was updated</param>
        /// <param name="sourceElementName">Source element name of the relationship</param>
        /// <param name="destinationElementName">Destination element name of the relationship</param>
        public RelationshipChangedEvent(string workspaceId, string relationshipId, string importId, string importKey, string? importLink, string sourceElementName, string destinationElementName)
        {
            WorkspaceId = workspaceId;
            RelationshipId = relationshipId;
            ImportId = importId;
            ImportKey = importKey;
            ImportLink = importLink;
            SourceElementName = sourceElementName;
            DestinationElementName = destinationElementName;
        }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of workspace in whitch relationship exists</param>
        /// <param name="relationshipId">The ID of relationship that was updated</param>
        /// <param name="importId">The ID of import during whitch relationship was updated</param>
        /// <param name="importKey">The key of import during whitch relationship was updated</param>
        /// <param name="importLink">The source link of import during whitch relationship was updated</param>
        /// <param name="sourceElementName">Source element name of the relationship</param>
        /// <param name="destinationElementName">Destination element name of the relationship</param>
        /// <param name="changes">Relationship changes details</param>
        public RelationshipChangedEvent(string workspaceId, string relationshipId, string importId, string importKey, string? importLink, string sourceElementName, string destinationElementName, IDictionary<string, Tuple<string, string>> changes)
        {
            WorkspaceId = workspaceId;
            RelationshipId = relationshipId;
            ImportId = importId;
            ImportKey = importKey;
            ImportLink = importLink;
            SourceElementName = sourceElementName;
            DestinationElementName = destinationElementName;
            Changes = changes;
        }
    }
}
