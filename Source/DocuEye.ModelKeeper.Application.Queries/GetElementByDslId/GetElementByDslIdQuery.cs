using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.ModelKeeper.Model;

using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetElementByDslId
{
    public class GetElementByDslIdQuery : IQuery<Element?>
    {
        /// <summary>
        /// Structurizr dsl identifier of Element
        /// </summary>
        public string DslId { get; set; }
        /// <summary>
        /// Workspace Id
        /// </summary>
        public string WorkspaceId { get; set; }
        /// <summary>
        /// Creates instance
        /// </summary>
        /// <param name="id">The ID of Element</param>
        /// <param name="workspaceId">Workspace Id</param>
        public GetElementByDslIdQuery(string dslId, string workspaceId)
        {
            this.DslId = dslId;
            this.WorkspaceId = workspaceId;
        }
    }
}
