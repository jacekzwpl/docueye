using DocuEye.ViewsKeeper.Application.Model;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DocuEye.ViewsKeeper.Model.Maps;
using DocuEye.Infrastructure.Mediator.Queries;

namespace DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement
{
    /// <summary>
    /// Handler for FindViewsWithElementQuery
    /// </summary>
    public class FindViewsWithElementQueryHandler : IQueryHandler<FindViewsWithElementQuery, IEnumerable<ViewWithElement>>
    {
        private readonly IViewsKeeperDBContext dbContext;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        public FindViewsWithElementQueryHandler(IViewsKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of elements</returns>
        public async Task<IEnumerable<ViewWithElement>> HandleAsync(FindViewsWithElementQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ViewWithElement>();
            var landScapeViews = await this.dbContext.SystemLandscapeViews
                .Find(o => o.ViewType == ViewType.SystemLandscapeView && o.WorkspaceId == request.WorkspaceId && o.Elements.Any(e => e.Id == request.ElementId));
            result.AddRange(landScapeViews.MapToViewWithElements());
            var systemContextViews = await this.dbContext.SystemContextViews
                .Find(o => o.ViewType == ViewType.SystemContextView && o.WorkspaceId == request.WorkspaceId &&  (o.SoftwareSystemId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(systemContextViews.MapToViewWithElements());
            var containerViews = await this.dbContext.ContainerViews
                .Find(o => o.ViewType == ViewType.ContainerView && o.WorkspaceId == request.WorkspaceId && (o.SoftwareSystemId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(containerViews.MapToViewWithElements());
            var componentViews = await this.dbContext.ComponentViews
                .Find(o => o.ViewType == ViewType.ComponentView && o.WorkspaceId == request.WorkspaceId && (o.ContainerId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(componentViews.MapToViewWithElements());
            var dynamicViews = await this.dbContext.DynamicViews
                .Find(o => o.ViewType == ViewType.DynamicView && o.WorkspaceId == request.WorkspaceId && (o.ElementId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(dynamicViews.MapToViewWithElements());
            var deploymentViews = await this.dbContext.DeploymentViews
                .Find(o => o.ViewType == ViewType.DeploymentView && o.WorkspaceId == request.WorkspaceId && (o.SoftwareSystemId == request.ElementId || o.Elements.Any(e => e.InstanceOfId == request.ElementId || e.Id == request.ElementId)));
            result.AddRange(deploymentViews.MapToViewWithElements());



            return result;

        }
    }
}
