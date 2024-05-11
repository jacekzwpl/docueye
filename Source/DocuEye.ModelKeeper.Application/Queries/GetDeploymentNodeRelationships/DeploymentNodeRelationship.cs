using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class DeploymentNodeRelationship
    {
        public string SourceNodeId { get; set; } = null!;
        public string SourceNodeName { get; set; } = null!;
        public string DestinationNodeId { get; set; } = null!;
        public string DestinationNodeName { get; set; } = null!;
        public IList<DeploymentNodeRelationshipItem> Items { get; set; } = null!;
        
    }
}
