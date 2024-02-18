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
            // Create new views
            if (request.SystemLandscapeViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.SystemLandscapeViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.SystemLandscapeView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.SystemLandscapeView);
                // Save views
                foreach(var view in request.SystemLandscapeViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if(existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.SystemLandscapeViews.UpsertOneAsync(view);
                }
                
            }else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.SystemLandscapeView);
            }

            if (request.SystemContextViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.SystemContextViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.SystemContextView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.SystemContextView);
                // Save views
                foreach (var view in request.SystemContextViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.SystemContextViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.SystemContextView);
            }

            if (request.ContainerViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.ContainerViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.ContainerView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.ContainerView);
                // Save views
                foreach (var view in request.ContainerViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.ContainerViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.ContainerView);
            }

            if (request.ComponentViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.ComponentViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.ComponentView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.ComponentView);
                // Save views
                foreach (var view in request.ComponentViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.ComponentViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.ComponentView);
            }

            if (request.DeploymentViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.DeploymentViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.DeploymentView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.DeploymentView);
                // Save views
                foreach (var view in request.DeploymentViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.DeploymentViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.DeploymentView);
            }

            if (request.DynamicViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.DynamicViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.DynamicView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.DynamicView);
                // Save views
                foreach (var view in request.DynamicViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.DynamicViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.DynamicView);
            }

            if (request.FilteredViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.FilteredViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.FilteredView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.FilteredView);
                // Save views
                foreach (var view in request.FilteredViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.FilteredViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.FilteredView);
            }

            if (request.ImagesViews.Count > 0)
            {
                //Delete all views that are missing in new sources
                var keys = request.ImagesViews.Select(o => o.Key).ToArray();
                await this.DeleteMissing(request.WorkspaceId, keys, ViewType.ImageView);
                // Get existing views 
                var existingViews = await this.GetExisting(request.WorkspaceId, ViewType.ImageView);
                // Save views
                foreach (var view in request.ImagesViews)
                {
                    var existingView = existingViews.SingleOrDefault(o => o.Key == view.Key);
                    if (existingView != null)
                    {
                        view.Id = existingView.Id;
                    }
                    await this.dbContext.ImageViews.UpsertOneAsync(view);
                }
            }
            else
            {
                await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == request.WorkspaceId
                    && o.ViewType == ViewType.ImageView);
            }
        }

        private async Task DeleteMissing(string workspaceId, string[] keys, string viewType)
        {
            await this.dbContext.AllViews.DeleteManyAsync(
                    o => o.WorkspaceId == workspaceId
                    && !keys.Contains(o.Key)
                    && o.ViewType == viewType);
        }

        private async Task<IEnumerable<BaseView>> GetExisting(string workspaceId, string viewType)
        {
            return await this.dbContext.AllViews
                    .Find(o => o.WorkspaceId == workspaceId
                    && o.ViewType == viewType);
        }
    }
}
