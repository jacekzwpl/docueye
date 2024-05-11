using MediatR;
using System.Collections.Generic;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class GetDeploymentNodeRelationshipsQuery : IRequest<IEnumerable<DeploymentNodeRelationship>>
    {
        public string WorkspaceId { get; set; }

        public GetDeploymentNodeRelationshipsQuery(string workspaceId) {  
            WorkspaceId = workspaceId; 
        }
    }
}
