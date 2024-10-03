using DocuEye.Infrastructure.HttpProblemDetails;
using DocuEye.WorkspacesKeeper.Application.Commands.DeleteWorkspace;
using DocuEye.WorkspacesKeeper.Application.Queries.FindWorspaces;
using DocuEye.WorkspacesKeeper.Application.Queries.GetThemeStyles;
using DocuEye.WorkspacesKeeper.Application.Queries.GetViewConfiguration;
using DocuEye.WorkspacesKeeper.Application.Queries.GetWorkspace;
using DocuEye.WorkspacesKeeper.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DocuEye.Workspaces.Api.Controllers
{
    /// <summary>
    /// Workspace controller 
    /// </summary>
    [Route("api/workspaces")]
    [ApiController]
    public class WorkspacesController : ControllerBase
    {
        private readonly IMediator mediator;
        private const string WorkspaceNotFound = "Workspace data not found.";
        private const string WorkspacNotFoundDetails = "Workspace data was not found in DocuEye database. Refresh page to try again.";
        private const string WorkspaceViewConfigurationNotFound = "Workspace view configuration not found.";
        private const string WorkspaceViewConfigurationNotFoundDetails = "Workspace view configuration was not found. This can happen when workspace import was not finished with success. Try reimport your workspace.";
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public WorkspacesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Gets workspace list
        /// </summary>
        /// <param name="name">Workspace name filter</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>List of workspaces matching criteria</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoundedWorkspace>>> Get([FromQuery] string? name = null, [FromQuery]int? limit = null, [FromQuery] int? skip = null)
        {
            var query = new FindWorkspacesQuery()
            {
                Name = name,
                Limit = limit,
                Skip = skip
            };
            var result = await this.mediator.Send<IEnumerable<FoundedWorkspace>>(query);

            return this.Ok(result);
        }
        /// <summary>
        /// Gets workspace data
        /// </summary>
        /// <param name="id">Workspace ID</param>
        /// <returns>Workspace data</returns>
        [Route("{id}")]
        [HttpGet]
        [Authorize( Policy = "Workspace" )]
        public async Task<ActionResult<Workspace>> Get([FromRoute]string id)
        {
            var query = new GetWorkspaceQuery(id);
            var result = await this.mediator.Send<Workspace?>(query);
            if(result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(WorkspaceNotFound, WorkspacNotFoundDetails));
            }
            return this.Ok(result);
        }
        /// <summary>
        /// Gets Workspace view configuration
        /// </summary>
        /// <param name="id">Workspace ID</param>
        /// <returns>Workspace view configuration</returns>
        [Route("{id}/viewconfiguration")]
        [HttpGet]
        [Authorize(Policy = "Workspace")]
        public async Task<ActionResult<ViewConfiguration>> GetViewConfiguration([FromRoute] string id)
        {
            var query = new GetViewConfigurationQuery(id);
            var result = await this.mediator.Send<ViewConfiguration?>(query);
            if (result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(WorkspaceViewConfigurationNotFound, WorkspaceViewConfigurationNotFoundDetails));
            }
            
            if(result.Themes != null && result.Themes.Any())
            {
                var elementStyles = new List<ElementStyle>();
                var relationshipStyles = new List<RelationshipStyle>();
                foreach (var theme in result.Themes)
                {
                    var themeQuery = new GetThemeStylesQuery(theme);
                    var themeStyles = await this.mediator.Send<ThemeStylesResult?>(themeQuery);
                    if(themeStyles != null && themeStyles.Elements != null)
                    {
                        elementStyles.AddRange(themeStyles.Elements);
                    }
                    if(themeStyles != null && themeStyles.Relationships != null)
                    {
                        relationshipStyles.AddRange(themeStyles.Relationships);
                    }
                }
                elementStyles.AddRange(result.ElementStyles);
                relationshipStyles.AddRange(result.RelationshipStyles);
                result.ElementStyles = elementStyles;
                result.RelationshipStyles = relationshipStyles;
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Removes all workspace data
        /// </summary>
        /// <param name="id">Workspace ID</param>
        /// <returns></returns>
        [Route("{id}")]
        [HttpDelete]
        [Authorize(AuthenticationSchemes = "BasicTokenAuthentication")]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult> Delete([FromRoute] string id)
        {
            var command = new DeleteWorkspaceCommand(id);
            await this.mediator.Send(command);
            return this.NoContent();
        }

    }
}
