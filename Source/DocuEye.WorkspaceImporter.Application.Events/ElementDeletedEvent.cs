

using DocuEye.Infrastructure.Mediator.Events;

namespace DocuEye.WorkspaceImporter.Application.Events
{
    /// <summary>
    /// Event that is fired when element should be removed according to detected changes during import
    /// </summary>
    public class ElementDeletedEvent : IEvent
    {
        /// <summary>
        /// The ID of workspace in whitch element is created
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// The ID of removed element
        /// </summary>
        public string ElementId { get; set; }
        /// <summary>
        /// The ID of import during whitch element was removed
        /// </summary>
        public string ImportId { get; set; }
        /// <summary>
        /// The key of import during whitch element was removed
        /// </summary>
        public string ImportKey { get; set; }
        /// <summary>
        /// The source link of import during whitch element was removed
        /// </summary>
        public string? ImportLink { get; set; }
        /// <summary>
        /// The name of element that was removed
        /// </summary>
        public string ElementName { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="workspaceId">The ID of workspace in whitch element is created</param>
        /// <param name="elementId">The ID of removed element</param>
        /// <param name="importId">The ID of import during whitch element was removed</param>
        /// <param name="importKey">The key of import during whitch element was removed</param>
        /// <param name="importLink">The source link of import during whitch element was removed</param>
        /// <param name="elementName">The name of element that was removed</param>
        public ElementDeletedEvent(string workspaceId, string elementId, string importId, string importKey, string? importLink, string elementName)
        {
            WorkspaceId = workspaceId;
            ElementId = elementId;
            ImportId = importId;
            ImportKey = importKey;
            ImportLink = importLink;
            ElementName = elementName;
        }
    }
}
