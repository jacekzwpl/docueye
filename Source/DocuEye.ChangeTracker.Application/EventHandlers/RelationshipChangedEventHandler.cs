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
    /// Handles RelationshipChangedEvent
    /// </summary>
    public class RelationshipChangedEventHandler : INotificationHandler<RelationshipChangedEvent>
    {
        private readonly IChangeTrackerDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public RelationshipChangedEventHandler(IChangeTrackerDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the event
        /// </summary>
        /// <param name="notification">event data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(RelationshipChangedEvent notification, CancellationToken cancellationToken)
        {

            var existingChange = await this.dbContext.ModelChanges
                .FindOne(o => o.ImportId == notification.ImportId
                    && o.RelationshipId == notification.RelationshipId
                    && o.WorkspaceId == notification.WorkspaceId);

            if (existingChange == null)
            {
                var descriptionBuilder = new StringBuilder();
                foreach (var key in notification.Changes.Keys)
                {
                    var values = notification.Changes[key];
                    descriptionBuilder.AppendLine(
                        string.Format("Property {0} of relationship between {1} and {2} changed value from '{3}' to '{4}'.",
                            key,
                            notification.SourceElementName,
                            notification.DestinationElementName,
                            values.Item1, values.Item2));
                }

                var description = descriptionBuilder.ToString();
                if (string.IsNullOrWhiteSpace(description))
                {
                    description = string.Format(
                        "Relationship between {0} and {1} has been updated.",
                        notification.SourceElementName,
                        notification.DestinationElementName);
                }

                await this.dbContext.ModelChanges.InsertOneAsync(new ModelChange()
                {
                    Id = Guid.NewGuid().ToString(),
                    WorkspaceId = notification.WorkspaceId,
                    RelationshipId = notification.RelationshipId,
                    EventTime = DateTime.UtcNow,
                    ImportId = notification.ImportId,
                    ImportKey = notification.ImportKey,
                    ImportLink = notification.ImportLink,
                    Description = description,
                    Type = ModelChangeType.RelationshipUpdated
                });
            }


        }
    }
}
