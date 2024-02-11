using MediatR;
using System;
using System.Collections.Generic;

namespace DocuEye.WorkspaceImporter.Application.Events
{
    /// <summary>
    /// Event that is fired when element should be updated according to detected changes during import
    /// </summary>
    public class ElementChangedEvent : INotification
    {
        /// <summary>
        /// Workspace ID
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Element ID
        /// </summary>
        public string ElementId { get; set; }
        /// <summary>
        /// Import Id
        /// </summary>
        public string ImportId { get; set; }
        /// <summary>
        /// Import Key
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// Import Link
        /// </summary>
        public string? ImportLink { get; set; }
        /// <summary>
        /// Details about element changes
        /// </summary>
        public IDictionary<string, Tuple<string, string>> Changes { get; set; } = new Dictionary<string, Tuple<string, string>>();
        
        public string ElementName { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="elementId">Element ID</param>
        /// <param name="importId">Import Id</param>
        /// <param name="importKey">Import Key</param>
        /// <param name="importLink">Import Link</param>
        public ElementChangedEvent(string workspaceId, string elementId, string importId, string importKey, string? importLink, string elementName)
        {
            WorkspaceId = workspaceId;
            ElementId = elementId;
            ImportId = importId;
            ImportKey = importKey;
            ImportLink = importLink;
            ElementName = elementName;  
        }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="elementId">Element ID</param>
        /// <param name="importId">Import Id</param>
        /// <param name="importKey">Import Key</param>
        /// <param name="importLink">Import Link</param>
        /// <param name="changes">Details about element changes</param>
        public ElementChangedEvent(string workspaceId, string elementId, string importId, string importKey, string? importLink, string elementName, IDictionary<string, Tuple<string, string>> changes)
        {
            WorkspaceId = workspaceId;
            ElementId = elementId;
            ImportId = importId;
            ImportKey = importKey;
            ImportLink = importLink;
            Changes = changes;
            ElementName = elementName;
        }
    }
}
