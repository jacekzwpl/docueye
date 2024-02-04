using DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile;
using DocuEye.DocsKeeper.Application.Queries.GetOpenApiFile;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.HttpProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Api.Controllers
{
    [Route("api/workspaces/{workspaceId}/documentationfiles")]
    [ApiController]
    public class DocumentationFilesController : ControllerBase
    {
        private readonly IMediator mediator;
        public DocumentationFilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("openapi/{elementId}")]
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> SaveOpenApiDocumentationFile(SaveOpenApiFileCommand command)
        {
            if(!new string[] { ".json",".yaml",".yml" }.Contains(Path.GetExtension(command.Name)))
            {
                return this.BadRequest(new BadRequestProblemDetails("File type not supported", "Supportet file types are \".json\",\".yaml\",\".yml\""));
            }

            await this.mediator.Send(command);
            return this.Ok();
        }

        [Route("openapi/{elementId}")]
        [HttpGet]
        public async Task<IActionResult> GetOpenApiFile([FromRoute] string workspaceId, [FromRoute] string elementId)
        {

            var query = new GetOpenApiFileQuery(workspaceId, elementId);
            var result = await this.mediator.Send<DocumentationFile?>(query);
            if (result == null || result.Content == null)
            {
                return this.NotFound(new NotFoundProblemDetails("Open API documentation not found."));
            }

            var arr = Convert.FromBase64String(result.Content);
            return this.File(arr, result.Type ?? "application/octet-stream", Path.GetFileName(result.Name));
        }
    }
}
