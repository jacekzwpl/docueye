
using DocuEye.Infrastructure.Mediator.Queries;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class GetDeploymentNodeRelationshipsQuery : IQuery<IEnumerable<DeploymentNodeRelationship>>
    {
        public string WorkspaceId { get; set; }

        public GetDeploymentNodeRelationshipsQuery(string workspaceId) {  
            WorkspaceId = workspaceId; 
        }
    }
}
