using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Application.Commands.ImportWorkspace;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Controllers
{
    /// <summary>
    /// Workspace Importer controller
    /// </summary>
    [Route("api/workspaces")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "BasicTokenAuthentication")]
    public class WorkspacesImportController : ControllerBase
    {
        private readonly IMediator mediator;
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="mediator">Mediator service</param>
        public WorkspacesImportController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        /// <summary>
        /// Imports workspace
        /// </summary>
        /// <param name="command">Import workspace command</param>
        /// <returns>Returns import result</returns>
        [Route("import")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> Import(ImportWorkspaceRequest data)
        {
            var command = new ImportWorkspaceCommand()
            {
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId,
                SourceLink = data.SourceLink,
                WorkspaceData = data.WorkspaceData
            };
            var result = await this.mediator.Send<ImportWorkspaceResult>(command);
            return this.Ok(new ImportWorkspaceResponse()
            {
                IsSuccess = result.IsSuccess,
                WorkspaceId = result.WorkspaceId,
                Message = result.Message
            });
        }

        [Route("import/start")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportStart(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/elements")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportElements(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/relationships")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportRelationships(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/views")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportViews(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/documentation")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportDocumentation(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/decision")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportDecision(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/image")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportImage(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }

        [Route("import/finish")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportFinish(ImportWorkspaceStartRequest data)
        {
            throw new System.NotImplementedException();
        }




    }
}
