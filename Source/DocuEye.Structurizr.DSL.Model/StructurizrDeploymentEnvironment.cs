using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.DSL.Model
{
    public class StructurizrDeploymentEnvironment
    {
        public string Identifier { get; private set; }
        public string Name { get; set; }

        public StructurizrDeploymentEnvironment(string identifier, string name)
        {
            Identifier = identifier;
            Name = name;
        }

        public StructurizrModelElement ToModelElement(string modelId = "")
        {
            return new StructurizrModelElement(modelId, this.Identifier)
            {
                ModelId = modelId,
                Identifier = Identifier,
                Name = Name,
                Type = StructurizrModelElementType.DeploymentEnvironment
            };
        }

        public void FromModelElement(StructurizrModelElement element)
        {
            this.Name = element.Name;
            this.Identifier = element.Identifier;
        }
    }
}
