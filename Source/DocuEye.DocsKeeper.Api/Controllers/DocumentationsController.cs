using DocuEye.DocsKeeper.Application.Queries.GetImage;
using DocuEye.DocsKeeper.Application.Queries.GetWorkspaceDocumentation;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.HttpProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Api.Controllers
{
    /// <summary>
    /// Documentations controller
    /// </summary>
    [Route("api/workspaces/{workspaceId}/documentations")]
    [ApiController]
    [Authorize(Policy = "Workspace")]
    public class DocumentationsController : ControllerBase
    {
        private readonly IMediator mediator;
        private const string DocumentationContentNotFound = "Documentation content not found.";
        private const string DocumentationContentNotFoundDetails = "Documentation content was not found in DocuEye database. This can happen when workspace import was treggered in the mean time. Try refresh page to load new configuration.";
        private const string ImageNotFound = "Image not found.";
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public DocumentationsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Gets documentation content for workspace
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="baseUrl">Host for download image uri</param>
        /// <returns>Documentation content</returns>
        [Route("worskpace")]
        [HttpGet]
        public async Task<ActionResult<DocumentationContent>> GetDocumentation([FromRoute] string workspaceId, [FromQuery] string baseUrl)
        {
            var query = new GetDocumentationContentQuery(workspaceId, baseUrl);
            var result = await this.mediator.Send<DocumentationContent?>(query);
            if (result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DocumentationContentNotFound, DocumentationContentNotFoundDetails));
            }
            return this.Ok(result);
        }
        /// <summary>
        /// Gets documentation content for element in workspace
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="elementId">Element Id</param>
        /// <param name="baseUrl">Host for download image uri</param>
        /// <returns></returns>
        [Route("element/{elementId}")]
        [HttpGet]
        public async Task<ActionResult<DocumentationContent>> GetDocumentation([FromRoute] string workspaceId, [FromRoute] string elementId, [FromQuery] string baseUrl)
        {
            var query = new GetDocumentationContentQuery(workspaceId, baseUrl, elementId);
            var result = await this.mediator.Send<DocumentationContent?>(query);
            if (result == null)
            {
                return this.NotFound(new NotFoundProblemDetails(DocumentationContentNotFound, DocumentationContentNotFoundDetails));
            }
            return this.Ok(result);
        }
        /// <summary>
        /// Gets the image for display
        /// </summary>
        /// <param name="workspaceId">Workspace Id</param>
        /// <param name="documentationId">Documentation Id</param>
        /// <param name="name">Name of the image</param>
        /// <returns>The image</returns>
        [Route("{documentationId}/images/{*name}")]
        [HttpGet]
        public async Task<IActionResult> GetDocumentationImageFile([FromRoute] string workspaceId, [FromRoute] string documentationId, [FromRoute] string name)
        {
            var query = new GetImageQuery(documentationId, workspaceId, name);
            var result = await this.mediator.Send<Image?>(query);
            if (result == null || result.Content == null)
            {
                return this.NotFound(new NotFoundProblemDetails(ImageNotFound));
            }

            var arr = Convert.FromBase64String(result.Content);
            return this.File(arr, result.Type ?? "application/octet-stream", Path.GetFileName(name));
        }
    }
}
