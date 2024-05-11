using DocuEye.ModelKeeper.Model;
using DocuEye.ModelKeeper.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class GetDeploymentNodeRelationshipsQueryHandler : IRequestHandler<GetDeploymentNodeRelationshipsQuery, IEnumerable<DeploymentNodeRelationship>>
    {
        private readonly IModelKeeperDBContext dbContext;

        public GetDeploymentNodeRelationshipsQueryHandler(IModelKeeperDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<DeploymentNodeRelationship>> Handle(GetDeploymentNodeRelationshipsQuery request, CancellationToken cancellationToken)
        {
            var nodes = await this.dbContext.Elements.Find(
                o => o.ParentId == null 
                && o.Type == ElementType.DeploymentNode
                && o.WorkspaceId == request.WorkspaceId);
            var elements = new List<DeploymentNodeElement>();
            foreach (var node in nodes)
            {
                elements.AddRange(await this.GetDeploymentNodeElements(node));
            }

            var relationships = await this.dbContext.Relationships.Find(
                o => o.WorkspaceId == request.WorkspaceId
                );
            var result = new List<DeploymentNodeRelationship>();
            foreach(var relationship in relationships)
            {
                var relSources = elements.Where(o => o.ElementId == relationship.SourceId).ToArray();
                var relDestinations = elements.Where(o => o.ElementId == relationship.DestinationId).ToArray();
                foreach(var source in relSources)
                {
                    foreach(var dest in relDestinations)
                    {
                        if(source.NodeId != dest.NodeId)
                        {
                            var resultRelationship = result.FirstOrDefault(o => o.SourceNodeId == source.NodeId && o.DestinationNodeId == dest.NodeId);
                            if(resultRelationship == null)
                            {
                                result.Add(new DeploymentNodeRelationship()
                                {
                                    SourceNodeId = source.NodeId,
                                    SourceNodeName = source.NodeName,
                                    DestinationNodeId = dest.NodeId,
                                    DestinationNodeName = dest.NodeName,
                                    Items = new List<DeploymentNodeRelationshipItem>() { 
                                        new DeploymentNodeRelationshipItem() { 
                                            Technology = relationship.Technology,
                                            Descriptions = new List<string>()
                                            {
                                                relationship.Description
                                            }
                                        } 
                                    }
                                });
                            }else
                            {
                                var item = resultRelationship.Items.FirstOrDefault(o => o.Technology == relationship.Technology);
                                if(item == null)
                                {
                                    resultRelationship.Items.Add(new DeploymentNodeRelationshipItem()
                                    {
                                        Technology = relationship.Technology,
                                        Descriptions = new List<string>()
                                            {
                                                relationship.Description
                                            }
                                    });
                                }else
                                {
                                    item.Descriptions.Add(relationship.Description);
                                }
                            }
                            
                        }
                    }
                }
            }

            return result;
        }

        private async Task<List<string>> GetInnerNodes(Element node)
        {
            var deploymentNodes = await this.dbContext.Elements.Find(
                o => o.Type == ElementType.DeploymentNode && o.ParentId == node.Id);
            var nodeIds = deploymentNodes.Select(o => o.Id).ToList();
            foreach(var innernode in deploymentNodes)
            {
                nodeIds.AddRange(await this.GetInnerNodes(innernode));
            }

            return nodeIds;
        }

        private async Task<IEnumerable<DeploymentNodeElement>> GetDeploymentNodeElements(Element node)
        {
            var ids = await this.GetInnerNodes(node);

            ids.Add(node.Id);
            var types = new List<string>()
            {
                ElementType.SoftwareSystemInstance,
                ElementType.ContainerInstance
            };

            var elements = await this.dbContext.Elements.Find(
                o => ids.Contains(o.ParentId) && types.Contains(o.Type));
            var result = new List<DeploymentNodeElement>();
            foreach( var element in elements)
            {
                result.Add(new DeploymentNodeElement()
                {
                    ElementId = element.InstanceOfId,
                    NodeId = node.Id,
                    NodeName = node.Name
                });
            }
            return result;
        }
    }
}
