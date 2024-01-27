using MediatR;

namespace DocuEye.WorkspaceImporter.Application.Events
{
    /// <summary>
    /// Event that is fired when element should be created according to detected changes during import
    /// </summary>
    public class ElementCreatedEvent : INotification
    {
        /// <summary>
        /// The ID of workspace in whitch element is created
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// The ID of newly created element
        /// </summary>
        public string ElementId { get; set; }
        /// <summary>
        /// The ID of import during whitch element was created
        /// </summary>
        public string ImportId { get; set; }
        /// <summary>
        /// New element name
        /// </summary>
        public string ElementName { get; set; }
        /// <summary>
        /// The key of import during whitch element was created
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// The source link of import during whitch element was created
        /// </summary>
        public string? ImportLink { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of workspace in whitch element is created</param>
        /// <param name="elementId">The ID of newly created element</param>
        /// <param name="importId">The ID of import during whitch element was created</param>
        /// <param name="elementName">New element name</param>
        /// <param name="importKey">The key of import during whitch element was created</param>
        /// <param name="importLink">The source link of import during whitch element was created</param>
        public ElementCreatedEvent(string workspaceId, string elementId, string importId, string elementName, string importKey, string? importLink)
        {
            WorkspaceId = workspaceId;
            ElementId = elementId;
            ImportId = importId;
            ElementName = elementName;
            ImportKey = importKey;
            ImportLink = importLink;
        }
    }
}
