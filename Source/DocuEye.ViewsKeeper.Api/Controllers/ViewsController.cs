using DocuEye.Infrastructure.HttpProblemDetails;
using DocuEye.Infrastructure.Mediator;
using DocuEye.ViewsKeeper.Api.Model;
using DocuEye.ViewsKeeper.Application.Commands.SaveViewLayout;
using DocuEye.ViewsKeeper.Application.Model;
using DocuEye.ViewsKeeper.Application.Queries.FindViewsWithElement;
using DocuEye.ViewsKeeper.Application.Queries.GetComponentView;
using DocuEye.ViewsKeeper.Application.Queries.GetContainerView;
using DocuEye.ViewsKeeper.Application.Queries.GetDeploymentView;
using DocuEye.ViewsKeeper.Application.Queries.GetDynamicView;
using DocuEye.ViewsKeeper.Application.Queries.GetFilteredView;
using DocuEye.ViewsKeeper.Application.Queries.GetImageView;
using DocuEye.ViewsKeeper.Application.Queries.GetSystemContextView;
using DocuEye.ViewsKeeper.Application.Queries.GetSystemLandscapeView;
using DocuEye.ViewsKeeper.Application.Queries.GetViewLayout;
using DocuEye.ViewsKeeper.Model;
using DocuEye.ViewsKeeper.Model.Maps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuEye.ViewsKeeper.Api.Controllers
{
    /// <summary>
    /// Views Controller
    /// </summary>
    [Route("api/workspaces/{workspaceId}/views")]
    [ApiController]
    [Authorize(Policy = "Workspace")]
    public class ViewsController : ControllerBase
    {

        private const string DiagramNotFound = "Diagram not found";
        private const string DiagramNotFoundDetails = "Diagram was not found in DocuEye database. This can happen when workspace import was treggered in the mean time. Try refresh page to load new configuration.";

        private readonly IMediator mediator;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public ViewsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Gets views that contains specific elements
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="elementId">The ID of element</param>
        /// <returns>List of views</returns>
        [Route("byelement/{elementId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewWithElement>>> GetViewsByElement([FromRoute] string workspaceId, [FromRoute] string elementId)
        {
            var query = new FindViewsWithElementQuery(elementId, workspaceId);
            var result = await this.mediator.SendQueryAsync<FindViewsWithElementQuery, IEnumerable<ViewWithElement>>(query);
            return this.Ok(result);
        }
        /// <summary>
        /// Gets system landscape view
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>System landscape view</returns>
        [Route("systemlandscape/{id}")]
        [HttpGet]
        public async Task<ActionResult<SystemLandscapeViewDiagram>> GetSystemLandscapeView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var viewQuery = new GetSystemLandscapeViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetSystemLandscapeViewQuery, SystemLandscapeView?>(viewQuery);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }

            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToSystemLandscapeViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets System context view
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>System context view</returns>
        [Route("systemcontext/{id}")]
        [HttpGet]
        public async Task<ActionResult<SystemContextViewDiagram>> GetSystemContextView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetSystemContextViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetSystemContextViewQuery, SystemContextView?>(query);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToSystemContextViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets container view
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>Container view</returns>
        [Route("container/{id}")]
        [HttpGet]
        public async Task<ActionResult<ContainerViewDiagram>> GetContainerView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetContainerViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetContainerViewQuery, ContainerView?>(query);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToContainerViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets Component view
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>Component view</returns>
        [Route("component/{id}")]
        [HttpGet]
        public async Task<ActionResult<ComponentViewDiagram>> GetComponentView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetComponentViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetComponentViewQuery, ComponentView?>(query);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToComponentViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets Dynamic View
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>Dynamic View</returns>
        [Route("dynamic/{id}")]
        [HttpGet]
        public async Task<ActionResult<DynamicViewDiagram>> GetDynamicView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetDynamicViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetDynamicViewQuery, DynamicView?>(query);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToDynamicViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets Deployment view
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>Dynamic view</returns>
        [Route("deployment/{id}")]
        [HttpGet]
        public async Task<ActionResult<DeploymentViewDiagram>> GetDeploymentView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetDeploymentViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetDeploymentViewQuery, DeploymentView?>(query);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToDeploymentViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets filtered view
        /// </summary>
        /// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>Filtered view</returns>
        [Route("filtered/{id}")]
        [HttpGet]
        public async Task<ActionResult<FilteredViewDiagram>> GetFilteredView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetFilteredViewQuery(id, workspaceId);
            var view = await this.mediator.SendQueryAsync<GetFilteredViewQuery, FilteredView?>(query);
            if (view == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            var layoutQuery = new GetViewLayoutQuery(workspaceId, id);
            var layout = await this.mediator.SendQueryAsync<GetViewLayoutQuery, ViewLayout?>(layoutQuery);

            var result = view.MapToFilteredViewDiagram();
            result.LayoutData = layout?.LayoutData;
            return this.Ok(result);
        }
        /// <summary>
        /// Gets image view
        /// </summary>
        //// <param name="workspaceId">The ID of workspace</param>
        /// <param name="id">The ID of view</param>
        /// <returns>Image view</returns>
        [Route("image/{id}")]
        [HttpGet]
        public async Task<ActionResult<ImageView>> GetImageView([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetImageViewQuery(id, workspaceId);
            var result = await this.mediator.SendQueryAsync<GetImageViewQuery, ImageView?>(query);
            if (result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DiagramNotFound, DiagramNotFoundDetails));
            }
            return this.Ok(result);
        }


        [Route("layout/{id}")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> SaveViewLayout([FromRoute] string workspaceId, [FromRoute] string id, [FromBody] SaveViewLayout data)
        {
            var command = new SaveViewLayoutCommand(workspaceId, id, data.LayoutData);
            await this.mediator.SendCommandAsync(command);
            return this.NoContent();
        }
    }
}
