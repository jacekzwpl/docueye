using DocuEye.DocsKeeper.Application.Queries.FindDecisions;
using DocuEye.DocsKeeper.Application.Queries.GetDecisionContent;
using DocuEye.Infrastructure.HttpProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Api.Controllers
{
    /// <summary>
    /// Decisions controller
    /// </summary>
    [Route("api/workspaces/{workspaceId}/decisions")]
    [ApiController]
    public class DecisionsController : ControllerBase
    {
        private readonly IMediator mediator;

        private const string DecisionNotFound = "Diagram not found";
        private const string DecisionNotFoundDetails = "Decision was not found in DocuEye database. This can happen when workspace import was treggered in the mean time. Try refresh page to load new configuration.";
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public DecisionsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Finds decisions for workspace
        /// </summary>
        /// <param name="workspaceId">Workspace id</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>List of decisions</returns>
        [Route("workspace")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoundedDecision>>> FindDecisions([FromRoute] string workspaceId, [FromQuery] int? limit = null, [FromQuery] int? skip = null)
        {
            var query = new FindDecisionsQuery(workspaceId, null, limit, skip);
            var result = await this.mediator.Send<IEnumerable<FoundedDecision>>(query);

            return this.Ok(result);
        }
        /// <summary>
        /// Finds decisions for element in workspace
        /// </summary>
        /// <param name="workspaceId">Workspace id</param>
        /// <param name="elementId">Element id</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>List of decisions</returns>
        [Route("element/{elementId}")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FoundedDecision>>> FindDecisions([FromRoute] string workspaceId, [FromRoute] string elementId, [FromQuery] int? limit = null, [FromQuery] int? skip = null)
        {
            var query = new FindDecisionsQuery(workspaceId, elementId, limit, skip);
            var result = await this.mediator.Send<IEnumerable<FoundedDecision>>(query);

            return this.Ok(result);
        }
        /// <summary>
        /// Gets content for decision
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="id">Decision Id</param>
        /// <param name="baseUrl">Host for download image uri</param>
        /// <returns>Decision content</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<DecisionContent>> GetWorkspaceDecision([FromRoute] string workspaceId, [FromRoute] string id, [FromQuery] string baseUrl)
        {
            var query = new GetDecisionQuery(workspaceId, id, baseUrl);
            var result = await this.mediator.Send<DecisionContent?>(query);
            if (result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DecisionNotFound, DecisionNotFoundDetails));
            }
            return this.Ok(result);
        }

    }
}
