using DocuEye.ChangeTracker.Model;
using DocuEye.ChangeTracker.Persistence;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.WorkspaceImporter.Application.Events;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ChangeTracker.Application.EventHandlers
{
    /// <summary>
    /// Handles ElementCreatedEvent
    /// </summary>
    public class ElementCreatedEventHandler : IEventHandler<ElementCreatedEvent>
    {
        private readonly IChangeTrackerDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public ElementCreatedEventHandler(IChangeTrackerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the event
        /// </summary>
        /// <param name="notification">event data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task HandleAsync(ElementCreatedEvent notification, CancellationToken cancellationToken)
        {

            var existingChange = await this.dbContext.ModelChanges
                .FindOne(o => o.ImportId == notification.ImportId
                    && o.ElementId == notification.ElementId
                    && o.WorkspaceId == notification.WorkspaceId);

            if (existingChange == null)
            {
                var description = string.Format("Element {0} has been created.", notification.ElementName);
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
                    Type = ModelChangeType.ElementCreated
                });
            }


        }
    }
}
