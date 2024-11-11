using DocuEye.WorkspaceImporter.Api.Model;
using DocuEye.WorkspaceImporter.Application.Commands.ClearDecisions;
using DocuEye.WorkspaceImporter.Application.Commands.ClearDocsItems;
using DocuEye.WorkspaceImporter.Application.Commands.FinalizeImport;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDecision;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDecisionsLinks;
using DocuEye.WorkspaceImporter.Application.Commands.ImportDocumentation;
using DocuEye.WorkspaceImporter.Application.Commands.ImportElements;
using DocuEye.WorkspaceImporter.Application.Commands.ImportImage;
using DocuEye.WorkspaceImporter.Application.Commands.ImportRelationships;
using DocuEye.WorkspaceImporter.Application.Commands.ImportViewConfiguration;
using DocuEye.WorkspaceImporter.Application.Commands.ImportViews;
using DocuEye.WorkspaceImporter.Application.Commands.StartImport;
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
        

        [Route("import/start")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportStart(ImportWorkspaceStartRequest data)
        {
            var command = new StartImportCommand()
            {
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId,
                SourceLink = data.SourceLink,
                WorkspaceName = data.WorkspaceName,
                WorkspaceDescription = data.WorkspaceDescription
            };
            var result = await this.mediator.Send<StartImportResult>(command);
            if(result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/viewconfiguration")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportViewConfiguration(ImportViewConfigurationRequest data)
        {
            var command = new ImportViewConfigurationCommand()
            {
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId,
                ViewConfiguration = data.ViewConfiguration
            };
            var result = await this.mediator.Send<ImportViewConfigurationResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/elements")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportElements(ImportElementsRequest data)
        {
            var command = new ImportElementsCommand()
            {
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId,
                Elements = data.Elements
            };
            var result = await this.mediator.Send<ImportElementsResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/relationships")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportRelationships(ImportRelationshipsRequest data)
        {
            var command = new ImportRelationshipsCommand()
            {
                Relationships = data.Relationships,
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId
            };
            var result = await this.mediator.Send<ImportRelationshipsResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/views")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportViews(ImportViewsRequest data)
        {
            var command = new ImportViewsCommand()
            {
                Views = data.Views,
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId
            };
            var result = await this.mediator.Send<ImportViewsResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/cleardocitems")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportClearDocItems(ImportClearDocItemsRequest data)
        {
            var command = new ClearDocsItemsCommand(data.WorkspaceId, data.ImportKey);
            var result = await this.mediator.Send<ClearDocsItemsResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/documentation")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportDocumentation(ImportDocumentationRequest data)
        {
            var command = new ImportDocumentationCommand(data.WorkspaceId, data.ImportKey, data.Documentation);
            var result = await this.mediator.Send<ImportDocumentationResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/decision")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportDecision(ImportDecisionRequest data)
        {
            var command = new ImportDecisionCommand(data.WorkspaceId, data.ImportKey, data.Decision);
            var result = await this.mediator.Send<ImportDecisionResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/image")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportImage(ImportImageRequest data)
        {
            var command = new ImportImageCommand(data.WorkspaceId, data.ImportKey, data.Image);
            var result = await this.mediator.Send<ImportImageResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/decisionlinks")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportDecisionLinks(ImportDecisionsLinksRequest data)
        {
            var command = new ImportDecisionsLinksCommand(data.WorkspaceId, data.ImportKey, data.DocumentationId, data.DecisionDslId, data.DecisionsLinks);
            var result = await this.mediator.Send<ImportDecisionsLinksResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/cleardecisions")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportClearDecisions(ImportClearDecisionsRequest data)
        {
            var command = new ClearDecisionsCommand(data.WorkspaceId, data.ImportKey);
            var result = await this.mediator.Send<ClearDecisionsResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }

        [Route("import/finish")]
        [HttpPut]
        [IgnoreAntiforgeryToken]
        public async Task<ActionResult<ImportWorkspaceResponse>> ImportFinish(ImportFinalizeRequest data)
        {
            var command = new FinalizeImportCommand()
            {
                ImportKey = data.ImportKey,
                WorkspaceId = data.WorkspaceId,
            };

            var result = await this.mediator.Send<FinalizeImportResult>(command);
            if (result.IsSuccess)
            {
                return this.Ok(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
            else
            {
                return this.BadRequest(new ImportWorkspaceResponse()
                {
                    IsSuccess = result.IsSuccess,
                    WorkspaceId = result.WorkspaceId,
                    Message = result.Message
                });
            }
        }
    }
}
