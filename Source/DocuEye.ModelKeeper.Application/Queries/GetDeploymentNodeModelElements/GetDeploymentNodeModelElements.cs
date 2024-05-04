using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeModelElements
{
    public class GetDeploymentNodeModelElements : IRequest<IEnumerable<DeploymentNodeModelElement>>
    {
        public string Id { get; set; }

        public GetDeploymentNodeModelElements(string id)
        {
            Id = id;
        }
    }
}
