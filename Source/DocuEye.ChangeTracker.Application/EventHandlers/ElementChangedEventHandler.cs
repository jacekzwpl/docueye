using DocuEye.ChangeTracker.Model;
using DocuEye.ChangeTracker.Persistence;
using DocuEye.WorkspaceImporter.Application.Events;
using MediatR;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.EventHandlers
{
    /// <summary>
    /// Handles ElementChangedEvent
    /// </summary>
    public class ElementChangedEventHandler : INotificationHandler<ElementChangedEvent>
    {
        private readonly IChangeTrackerDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public ElementChangedEventHandler(IChangeTrackerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the event
        /// </summary>
        /// <param name="notification">event data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(ElementChangedEvent notification, CancellationToken cancellationToken)
        {

            var existingChange = await this.dbContext.ModelChanges
                .FindOne(o => o.ImportId == notification.ImportId 
                    && o.ElementId == notification.ElementId
                    && o.WorkspaceId == notification.WorkspaceId);
            
            if(existingChange == null)
            {
                var descriptionBuilder = new StringBuilder();
                foreach(var key in notification.Changes.Keys)
                {
                    var values = notification.Changes[key];
                    descriptionBuilder.Append(
                        string.Format("Property {0} changed value from '{1}' to '{2}'.",
                            key, values.Item1, values.Item2));
                }

                var description = descriptionBuilder.ToString();
                if(string.IsNullOrWhiteSpace(description))
                {
                    description = string.Format(
                        "Element {0} has been updated.", 
                        notification.ElementName);
                }

                await this.dbContext.ModelChanges.InsertOneAsync(new ModelChange()
                {
                    Id = Guid.NewGuid().ToString(),
                    WorkspaceId = notification.WorkspaceId,
                    ElementId = notification.ElementId,
                    EventTime = DateTime.UtcNow,
                    ImportId = notification.ImportId,
                    ImportKey = notification.ImportKey,
                    ImportLink = notification.ImportLink,
                    Description = description,
                    Type = ModelChangeType.ElementUpdated
                });
            }

            
        }
    }
}
