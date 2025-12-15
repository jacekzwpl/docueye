using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrDeploymentGroup
    {
        public string Identifier { get; }
        public string Name { get; set; }
        public string DeploymentEnvironemntIdentifier { get; set; }

        public StructurizrDeploymentGroup(string identifier, string name, string deploymentEnvironemntIdentifier)
        {
            Identifier = identifier;
            Name = name;
            DeploymentEnvironemntIdentifier = deploymentEnvironemntIdentifier;
        }
    }
}
