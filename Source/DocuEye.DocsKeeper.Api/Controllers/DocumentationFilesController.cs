using DocuEye.DocsKeeper.Application.Queries.GetDocumentationFile;
using DocuEye.DocsKeeper.Application.Queries.GetOpenApiFile;
using DocuEye.DocsKeeper.Model;
using DocuEye.Infrastructure.HttpProblemDetails;
using DocuEye.Infrastructure.Mediator;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DocuEye.DocsKeeper.Api.Controllers
{
    [Route("api/workspaces/{workspaceId}/documentationfiles")]
    [ApiController]
    [Authorize(Policy = "Workspace")]
    public class DocumentationFilesController : ControllerBase
    {
        private readonly IMediator mediator;
        public DocumentationFilesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [Route("{elementId}")]
        [HttpGet]
        public async Task<IActionResult> GetDocumentationFile([FromRoute] string workspaceId, [FromRoute] string elementId, [FromQuery] string documentationType)
        {
            var query = new GetDocumentationFileQuery
            {
                WorkspaceId = workspaceId,
                ElementId = elementId,
                DocumentationType = documentationType
            };
            var result = await this.mediator
                .SendQueryAsync<GetDocumentationFileQuery, DocumentationFile?>(query);
            if (result == null || result.Content == null)
            {
                return this.NotFound(new NotFoundProblemDetails("Documentation file not found."));
            }

            var arr = Convert.FromBase64String(result.Content);
            return this.File(arr, result.MediaType ?? "application/octet-stream", Path.GetFileName(result.Name));
        }



        //[Route("openapi/{elementId}")]
        //[HttpGet]
        //public async Task<IActionResult> GetOpenApiFile([FromRoute] string workspaceId, [FromRoute] string elementId)
        //{

        //    var query = new GetOpenApiFileQuery(workspaceId, elementId);
        //    var result = await this.mediator.SendQueryAsync<GetOpenApiFileQuery,DocumentationFile?>(query);
        //    if (result == null || result.Content == null)
        //    {
        //        return this.NotFound(new NotFoundProblemDetails("Open API documentation not found."));
        //    }

        //    var arr = Convert.FromBase64String(result.Content);
        //    return this.File(arr, result.MediaType ?? "application/octet-stream", Path.GetFileName(result.Name));
        //}
    }
}
