using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class DeploymentNodeRelationship
    {
        public string SourceNodeId {  get; set; }
        public string SourceNodeName { get; set; }
        public string DestinationNodeId { get; set; }
        public string DestinationNodeName { get; set; }
        public string RelationshipTechnology {  get; set; }
        public string RelationshipDescription { get; set; }
        
    }
}
