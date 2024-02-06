using DocuEye.DocsKeeper.Application.Commands.SaveOpenApiFile;
using DocuEye.Infrastructure.HttpProblemDetails;
using DocuEye.WorkspaceImporter.Api.Model;
using MediatR;
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
    [Authorize]
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
        
        [Route("openapi/{elementId}")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> SaveOpenApiDocumentationFile([FromRoute]string workspaceId, [FromRoute]string elementId, ImportOpenApiFileRequest data)
        {
            if (!new string[] { ".json", ".yaml", ".yml" }.Contains(Path.GetExtension(data.Name)))
            {
                return this.BadRequest(new BadRequestProblemDetails("File type not supported", "Supportet file types are \".json\",\".yaml\",\".yml\""));
            }

            var command = new SaveOpenApiFileCommand(workspaceId, elementId, data.Content, data.Name);
            await this.mediator.Send(command);
            return this.NoContent();
        }
    }
}
