using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.ModelKeeper.Application.Queries.GetDeploymentNodeRelationships
{
    public class DeploymentNodeRelationshipItem
    {
        public string Technology {  get; set; }
        public IList<string> Descriptions { get; set; }
    }
}
