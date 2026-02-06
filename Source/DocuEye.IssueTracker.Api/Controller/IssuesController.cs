using DocuEye.Infrastructure.Mediator;
using DocuEye.IssueTracker.Application.Queries.GetWorkspaceIssues;
using DocuEye.IssueTracker.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.IssueTracker.Api.Controller
{
    [Route("api/workspaces/{workspaceId}/issues")]
    [ApiController]
    [Authorize(Policy = "Workspace")]
    public class IssuesController : ControllerBase
    {
        private readonly IMediator mediator;

        public IssuesController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Issue>>> Get([FromRoute] string workspaceId)
        {
            var query = new GetWorkspaceIssuesQuery(workspaceId);
            var result = await this.mediator.SendQueryAsync<GetWorkspaceIssuesQuery, IEnumerable<Issue>>(query);
            return this.Ok(result);
        }
    }
}
