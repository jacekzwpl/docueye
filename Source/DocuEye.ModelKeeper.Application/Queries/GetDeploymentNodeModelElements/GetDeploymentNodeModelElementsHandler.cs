using AutoMapper;
using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeModelElements
{
    public class GetDeploymentNodeModelElementsHandler : IRequestHandler<GetDeploymentNodeModelElements, IEnumerable<DeploymentNodeModelElement>>
    {
        private readonly IModelKeeperDBContext dbContext;
        private readonly IMapper mapper;

        public GetDeploymentNodeModelElementsHandler(IModelKeeperDBContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<DeploymentNodeModelElement>> Handle(GetDeploymentNodeModelElements request, CancellationToken cancellationToken)
        {
         
            var deploymentNodes = await this.dbContext.Elements.Find(
                o => o.Type == ElementType.DeploymentNode && o.ParentId == request.Id);

            var ids = deploymentNodes.Select(o => o.Id).ToList();
            ids.Add(request.Id);
            var types = new List<string>()
            {
                ElementType.SoftwareSystemInstance,
                ElementType.ContainerInstance
            };

            var elements = await this.dbContext.Elements.Find(
                o => ids.Contains(o.ParentId) && types.Contains(o.Type));

            return this.mapper.Map<IEnumerable<DeploymentNodeModelElement>>(elements);
        }
    }
}
