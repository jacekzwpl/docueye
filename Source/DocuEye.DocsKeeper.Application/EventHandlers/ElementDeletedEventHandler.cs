using DocuEye.DocsKeeper.Persistence;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.WorkspaceImporter.Application.Events;

using System;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Application.EventHandlers
{
    /// <summary>
    /// Handles ElementDeletedEvent
    /// </summary>
    public class ElementDeletedEventHandler : IEventHandler<ElementDeletedEvent>
    {
        private readonly IDocsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB Context</param>
        public ElementDeletedEventHandler(IDocsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles the event
        /// </summary>
        /// <param name="notification">event data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task HandleAsync(ElementDeletedEvent notification, CancellationToken cancellationToken)
        {

            //Delete ll documentation files connected to element
            await this.dbContext.DocumentationFiles
                .DeleteManyAsync(o => o.WorkspaceId == notification.WorkspaceId
                && o.ElementId == notification.ElementId);
        }
    }
}
