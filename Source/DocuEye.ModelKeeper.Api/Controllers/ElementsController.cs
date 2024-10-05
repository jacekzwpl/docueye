using DocuEye.Infrastructure.HttpProblemDetails;
using DocuEye.ModelKeeper.Application.Queries.GetChildElements;
using DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships;
using DocuEye.ModelKeeper.Application.Queries.GetElement;
using DocuEye.ModelKeeper.Application.Queries.GetElementConsumers;
using DocuEye.ModelKeeper.Application.Queries.GetElementDependences;
using DocuEye.ModelKeeper.Application.Queries.GetWorspaceCatalogElements;
using DocuEye.ModelKeeper.Model;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Api.Controllers
{
    /// <summary>
    /// Elements controller
    /// </summary>
    [Route("api/workspaces/{workspaceId}/elements")]
    [ApiController]
    [Authorize(Policy = "Workspace")]
    public class ElementsController : ControllerBase
    {
        private readonly IMediator mediator;
        private const string ElementNotFound = "Element data not found.";
        private const string ElementNotFoundDetails = "Element data was not found in DocuEye database. This can happen when workspace import was treggered in the mean time. Go back to element list to verify that element still exists.";
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public ElementsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Gets workspace elements
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="name">Elements name or its part</param>
        /// <param name="type">Elements type</param>
        /// <param name="limit">Maximum number of items that will be returned</param>
        /// <param name="skip">Number of items to skip</param>
        /// <returns>List of elements</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WorkspaceCatalogElement>>> Get([FromRoute] string workspaceId, [FromQuery] string? name = null, [FromQuery] string? type = null, [FromQuery] int? limit = null, [FromQuery] int? skip = null)
        {
            var query = new GetWorspaceCatalogElementsQuery(workspaceId, name, type, limit, skip);
            var result = await this.mediator.Send<IEnumerable<WorkspaceCatalogElement>>(query);
            return this.Ok(result);
        }
        /// <summary>
        /// Gets element data
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="id">Element Id</param>
        /// <returns>Element data</returns>
        [Route("{id}")]
        [HttpGet]
        public async Task<ActionResult<Element>> Get([FromRoute] string workspaceId, [FromRoute] string id)
        {
            var query = new GetElementQuery(id, workspaceId);
            var result = await this.mediator.Send<Element?>(query);
            if (result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(ElementNotFound, ElementNotFoundDetails));
            }
            return this.Ok(result);
        }
        /// <summary>
        /// Gets elements children 
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="id">Element Id</param>
        /// <param name="type">Type of children to be returned</param>
        /// <returns>List of child elements</returns>
        [Route("{id}/children")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChildElement>>> GetChildren([FromRoute] string workspaceId, [FromRoute] string id, [FromQuery] string? type = null)
        {
            var query = new GetChildElementsQuery(workspaceId, id, type);
            var result = await this.mediator.Send<IEnumerable<ChildElement>>(query);
            return this.Ok(result);
        }
        /// <summary>
        /// Gets Elements dependences
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="id">Element Id</param>
        /// <param name="getLinked">Indicates if linked relationships should be included in dependeces result</param>
        /// <returns></returns>
        [Route("{id}/dependences")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementDependence>>> GetElementDependences([FromRoute] string workspaceId, [FromRoute] string id, [FromQuery] bool getLinked = false)
        {
            var query = new GetElementDependencesQuery(id, workspaceId, getLinked);
            var result = await this.mediator.Send<IEnumerable<ElementDependence>>(query);
            return this.Ok(result);
        }
        /// <summary>
        /// Gets consumers for element
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="id">Element Id</param>
        /// <param name="getLinked">Indicates if linked relationships should be included in consumers result</param>
        /// <returns></returns>
        [Route("{id}/consumers")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ElementConsumer>>> GetElementConsumers([FromRoute] string workspaceId, [FromRoute] string id, [FromQuery] bool getLinked = false)
        {
            var query = new GetElementConsumersQuery(id, workspaceId, getLinked);
            var result = await this.mediator.Send<IEnumerable<ElementConsumer>>(query);
            return this.Ok(result);
        }

        [Route("deploymentnodesmatrix")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeploymentNodeRelationship>>> GetDeploymentNodeRelationship([FromRoute] string workspaceId)
        {
            var query = new GetDeploymentNodeRelationshipsQuery(workspaceId);
            var result = await this.mediator.Send<IEnumerable<DeploymentNodeRelationship>>(query);
            return this.Ok(result);
        }


    }
}
