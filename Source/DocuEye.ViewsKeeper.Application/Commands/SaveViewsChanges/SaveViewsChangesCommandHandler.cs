using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Commands.SaveViewsChanges
{
    /// <summary>
    /// Handler for SaveViewsChangesCommand
    /// </summary>
    public class SaveViewsChangesCommandHandler : IRequestHandler<SaveViewsChangesCommand>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public SaveViewsChangesCommandHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles command
        /// </summary>
        /// <param name="request">command request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        public async Task Handle(SaveViewsChangesCommand request, CancellationToken cancellationToken)
        {
            //Delete all views
            await this.dbContext.AllViews.DeleteManyAsync(o => o.WorkspaceId == request.WorkspaceId);
            // Create new views
            if (request.SystemLandscapeViews.Count > 0)
            {
                await this.dbContext.SystemLandscapeViews.InsertManyAsync(request.SystemLandscapeViews);
            }

            if (request.SystemContextViews.Count > 0)
            {
                await this.dbContext.SystemContextViews.InsertManyAsync(request.SystemContextViews);
            }

            if (request.ContainerViews.Count > 0)
            {
                await this.dbContext.ContainerViews.InsertManyAsync(request.ContainerViews);
            }

            if (request.ComponentViews.Count > 0)
            {
                await this.dbContext.ComponentViews.InsertManyAsync(request.ComponentViews);
            }

            if (request.DeploymentViews.Count > 0)
            {
                await this.dbContext.DeploymentViews.InsertManyAsync(request.DeploymentViews);
            }

            if (request.DynamicViews.Count > 0)
            {
                await this.dbContext.DynamicViews.InsertManyAsync(request.DynamicViews);
            }

            if (request.FilteredViews.Count > 0)
            {
                await this.dbContext.FilteredViews.InsertManyAsync(request.FilteredViews);
            }

            if (request.ImagesViews.Count > 0)
            {
                await this.dbContext.ImageViews.InsertManyAsync(request.ImagesViews);
            }
        }
    }
}
