using DocuEye.DocsKeeper.Application.Commads.DeleteDocumentationFile;
using DocuEye.DocsKeeper.Application.Commads.SaveDocumentationFile;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.HttpProblemDetails;
using DocuEye.Infrastructure.Mediator;
using DocuEye.ModelKeeper.Application.Queries.GetElementByDslId;
using DocuEye.ModelKeeper.Model;
using DocuEye.WorkspaceImporter.Api.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Controllers
{
    [Route("api/workspaces/{workspaceId}/docfile")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicTokenAuthentication")]
    public class DocumentationFilesImportController : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public DocumentationFilesImportController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        /// <summary>
        /// Imports and saves a documentation file for a specified workspace element.
        /// </summary>
        /// <remarks>Supported file types are ".json", ".yaml", and ".yml". Either <c>ElementId</c> or
        /// <c>ElementDslId</c> must be provided in the request. The documentation type must be one of the supported
        /// types defined in <c>DocumentationFileType.AllTypes</c>.</remarks>
        /// <param name="workspaceId">The unique identifier of the workspace in which the documentation file will be saved.</param>
        /// <param name="data">The request containing the documentation file content, file name, element identifier, and documentation
        /// type.</param>
        /// <returns>An <see cref="IActionResult"/> indicating the result of the operation. Returns 204 No Content if the file is
        /// saved successfully; otherwise, returns an error response describing the failure.</returns>
        [Route("")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveDocumentationFile([FromRoute] string workspaceId, ImportDocumentationFileRequest data)
        {
            if (!new string[] { ".json", ".yaml", ".yml" }.Contains(Path.GetExtension(data.Name)))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "File type not supported",
                    "Supported file types are \".json\",\".yaml\",\".yml\""));
            }

            if (string.IsNullOrEmpty(data.ElementId) && string.IsNullOrEmpty(data.ElementDslId))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "Element identifier is missing",
                    "To import documentation file ElementId or ElementDslId must be provided."));
            }

            if(!new string[] { "openapi", "asyncapi" }.Contains(data.DocumentationType))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "Documentation File Type is not supported",
                    "Supported documentation types are: openapi, asyncapi"));
            }

            var elementId = string.Empty;

            if (!string.IsNullOrEmpty(data.ElementId))
            {
                elementId = data.ElementId;
            }
            else
            {
                var query = new GetElementByDslIdQuery(data.ElementDslId ?? "", workspaceId);
                var element = await this.mediator.SendQueryAsync<GetElementByDslIdQuery, Element?>(query);
                if (element == null)
                {
                    return this.NotFound(new NotFoundProblemDetails(
                        "Element not found",
                        string.Format("Element with dsl id = {0} was not found in workspace.", data.ElementDslId)));
                }
                elementId = element.Id;
            }

            var command = new SaveDocumentationFileCommand(
                workspaceId,
                elementId,
                data.Content,
                data.Name,
                DocumentationFileType.MapFromDocumentationType(data.DocumentationType));
            await this.mediator.SendCommandAsync(command);
            return this.NoContent();
        }

        [Route("")]
        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteDocumentationFile([FromRoute] string workspaceId, [FromQuery] string? elementId, [FromQuery] string? elementDslId, [FromQuery] string documentationType)
        {
            if (string.IsNullOrEmpty(elementId) && string.IsNullOrEmpty(elementDslId))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "Element identifier is missing",
                    "To delete openapi definition ElementId or ElementDslId must be provided."));
            }

            if (!new string[] { "openapi", "asyncapi" }.Contains(documentationType))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "Documentation File Type is not supported",
                    "Supported documentation types are: openapi, asyncapi"));
            }

            if (string.IsNullOrEmpty(elementId))
            {
                var query = new GetElementByDslIdQuery(elementDslId ?? "", workspaceId);
                var element = await this.mediator.SendQueryAsync<GetElementByDslIdQuery, Element?>(query);
                if (element == null)
                {
                    return this.NotFound(new NotFoundProblemDetails(
                        "Element not found",
                        string.Format("Element with dsl id = {0} was not found in workspace.", elementDslId)));
                }
                elementId = element.Id;
            }

            var command = new DeleteDocumentationFileCommand(
                workspaceId, 
                elementId, 
                DocumentationFileType.MapFromDocumentationType(documentationType));
            await this.mediator.SendCommandAsync(command);
            return this.NoContent();
        }


        
    }
}
