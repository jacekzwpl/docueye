using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
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
            //Delete missing 
            List<string> existingIds = new List<string>();
            existingIds.AddRange(request.SystemLandscapeViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.SystemContextViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.ContainerViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.ComponentViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.DeploymentViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.DynamicViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.FilteredViews.Select(o => o.Id).ToArray());
            existingIds.AddRange(request.ImagesViews.Select(o => o.Id).ToArray());
            await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && !existingIds.Contains(o.Id));


            // Create new views
            if (request.SystemLandscapeViews.Count > 0)
            {
                foreach (var view in request.SystemLandscapeViews)
                {
                    await this.dbContext.SystemLandscapeViews.UpsertOneAsync(view);
                }
            }

            if (request.SystemContextViews.Count > 0)
            {
                foreach (var view in request.SystemContextViews)
                {
                    await this.dbContext.SystemContextViews.UpsertOneAsync(view);
                }
            }

            if (request.ContainerViews.Count > 0)
            {
                foreach (var view in request.ContainerViews)
                {
                    await this.dbContext.ContainerViews.UpsertOneAsync(view);
                }
            }

            if (request.ComponentViews.Count > 0)
            {
                foreach (var view in request.ComponentViews)
                {
                    await this.dbContext.ComponentViews.UpsertOneAsync(view);
                }
            }

            if (request.DeploymentViews.Count > 0)
            {
                foreach (var view in request.DeploymentViews)
                {
                    await this.dbContext.DeploymentViews.UpsertOneAsync(view);
                }
            }

            if (request.DynamicViews.Count > 0)
            {
                foreach (var view in request.DynamicViews)
                {
                    await this.dbContext.DynamicViews.UpsertOneAsync(view);
                }
            }

            if (request.FilteredViews.Count > 0)
            {
                foreach (var view in request.FilteredViews)
                {
                    await this.dbContext.FilteredViews.UpsertOneAsync(view);
                }
            }

            if (request.ImagesViews.Count > 0)
            {
                foreach (var view in request.ImagesViews)
                {
                    await this.dbContext.ImageViews.UpsertOneAsync(view);
                }
            }
        }

    }
}
