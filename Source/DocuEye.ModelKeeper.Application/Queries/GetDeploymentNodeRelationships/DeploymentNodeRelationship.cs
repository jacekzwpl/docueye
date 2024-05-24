using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class DeploymentNodeRelationship
    {
        public string SourceNodeId { get; set; } = null!;
        public string SourceNodeName { get; set; } = null!;
        public string? SourceNodeTechnology { get; set; }
        public string DestinationNodeId { get; set; } = null!;
        public string DestinationNodeName { get; set; } = null!;
        public string? DestinationTechnology { get; set; }
        public IList<DeploymentNodeRelationshipItem> Items { get; set; } = null!;
        
    }
}
