using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class DeploymentNodeElement
    {
        public string NodeId { get; set; } = null!;
        public string ElementId { get; set; } = null!;
        public string NodeName { get; set; } = null!;
        public string? Technology { get; set; } = null!;
    }
}
