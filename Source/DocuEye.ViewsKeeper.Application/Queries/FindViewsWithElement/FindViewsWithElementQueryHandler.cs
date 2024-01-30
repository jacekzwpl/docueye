using AutoMapper;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement
{
    /// <summary>
    /// Handler for FindViewsWithElementQuery
    /// </summary>
    public class FindViewsWithElementQueryHandler : IRequestHandler<FindViewsWithElementQuery, IEnumerable<ViewWithElement>>
    {
        private readonly IViewsKeeperDBContext dbContext;
        private readonly IMapper mapper;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="dbContext">MongoDB context</param>
        /// <param name="mapper">Automapper service</param>
        public FindViewsWithElementQueryHandler(IViewsKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }
        /// <summary>
        /// Handles query
        /// </summary>
        /// <param name="request">Query request data</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns>List of elements</returns>
        public async Task<IEnumerable<ViewWithElement>> Handle(FindViewsWithElementQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ViewWithElement>();
            var landScapeViews = await this.dbContext.SystemLandscapeViews
                .Find(o => o.ViewType == ViewType.SystemLandscapeView && o.WorkspaceId == request.WorkspaceId && o.Elements.Any(e => e.Id == request.ElementId));
            result.AddRange(this.mapper.Map<IEnumerable<ViewWithElement>>(landScapeViews));
            var systemContextViews = await this.dbContext.SystemContextViews
                .Find(o => o.ViewType == ViewType.SystemContextView && o.WorkspaceId == request.WorkspaceId &&  (o.SoftwareSystemId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(this.mapper.Map<IEnumerable<ViewWithElement>>(systemContextViews));
            var containerViews = await this.dbContext.ContainerViews
                .Find(o => o.ViewType == ViewType.ContainerView && o.WorkspaceId == request.WorkspaceId && (o.SoftwareSystemId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(this.mapper.Map<IEnumerable<ViewWithElement>>(containerViews));
            var componentViews = await this.dbContext.ComponentViews
                .Find(o => o.ViewType == ViewType.ComponentView && o.WorkspaceId == request.WorkspaceId && (o.ContainerId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(this.mapper.Map<IEnumerable<ViewWithElement>>(componentViews));
            var dynamicViews = await this.dbContext.DynamicViews
                .Find(o => o.ViewType == ViewType.DynamicView && o.WorkspaceId == request.WorkspaceId && (o.ElementId == request.ElementId || o.Elements.Any(e => e.Id == request.ElementId)));
            result.AddRange(this.mapper.Map<IEnumerable<ViewWithElement>>(dynamicViews));
            var deploymentViews = await this.dbContext.DeploymentViews
                .Find(o => o.ViewType == ViewType.DeploymentView && o.WorkspaceId == request.WorkspaceId && (o.SoftwareSystemId == request.ElementId || o.Elements.Any(e => e.InstanceOfId == request.ElementId || e.Id == request.ElementId)));
            result.AddRange(this.mapper.Map<IEnumerable<ViewWithElement>>(deploymentViews));



            return result;

        }
    }
}
