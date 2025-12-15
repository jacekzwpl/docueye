using DocuEye.DocsKeeper.Application.Commads.DeleteOpenApiFile;
using DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        /// Imports openapi definition for element in workspace
        /// </summary>
        /// <param name="workspaceId">Workspace ID</param>
        /// <param name="data">Request data</param>
        /// <returns></returns>
        [Route("openapi")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveOpenApiDocumentationFile([FromRoute]string workspaceId, ImportOpenApiFileRequest data)
        {
            if (!new string[] { ".json", ".yaml", ".yml" }.Contains(Path.GetExtension(data.Name)))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "File type not supported", 
                    "Supported file types are \".json\",\".yaml\",\".yml\""));
            }

            if(string.IsNullOrEmpty(data.ElementId) && string.IsNullOrEmpty(data.ElementDslId))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "Element identifier is missing", 
                    "To import openapi definition ElementId or ElementDslId must be provided."));
            }
            var elementId = string.Empty;
            
            if(!string.IsNullOrEmpty(data.ElementId))
            {
                elementId = data.ElementId;
            }else
            {
                var query = new GetElementByDslIdQuery(data.ElementDslId ?? "", workspaceId);
                var element = await this.mediator.SendQueryAsync<GetElementByDslIdQuery,Element?>(query);
                if(element == null)
                {
                    return this.NotFound(new NotFoundProblemDetails(
                        "Element not found", 
                        string.Format("Element with dsl id = {0} was not found in workspace.", data.ElementDslId)));
                }
                elementId = element.Id;
            }

            var command = new SaveOpenApiFileCommand(workspaceId, elementId, data.Content, data.Name);
            await this.mediator.SendCommandAsync(command);
            return this.NoContent();
        }

        [Route("openapi")]
        [HttpDelete]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteOpenApiDocumentationFile([FromRoute]string workspaceId, [FromQuery]string? elementId, [FromQuery]string? elementDslId)
        {
            if (string.IsNullOrEmpty(elementId) && string.IsNullOrEmpty(elementDslId))
            {
                return this.BadRequest(new BadRequestProblemDetails(
                    "Element identifier is missing",
                    "To delete openapi definition ElementId or ElementDslId must be provided."));
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

            var command = new DeleteOpenApiFileCommand(workspaceId, elementId);
            await this.mediator.SendCommandAsync(command);
            return this.NoContent();
        }
    }
}
