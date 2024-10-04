using DocuEye.ChangeTracker.Application.Queries.FindModelChanges;
using DocuEye.ChangeTracker.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DocuEye.ChangeTracker.Api.Controllers
{
    /// <summary>
    /// Controller for model changes
    /// </summary>
    [Route("api/{workspaceId}/modelchanges")]
    [ApiController]
    [Authorize(Policy = "Workspace")]
    public class ModelChangesController : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public ModelChangesController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Finds model changes for workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>Lists of items matching criteria</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ModelChange>>> Get([FromRoute] string workspaceId, [FromQuery] int? limit = null, [FromQuery] int? skip = null)
        {
            var query = new FindModelChangesQuery()
            {
                WorkspaceId = workspaceId,
                Limit = limit,
                Skip = skip
            };
            var result = await this.mediator.Send<IEnumerable<ModelChange>>(query);

            return this.Ok(result);
        }
    }
}
