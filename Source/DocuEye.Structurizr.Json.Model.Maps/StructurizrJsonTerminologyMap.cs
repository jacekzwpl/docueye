using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.Structurizr.Json.Model.Maps
{
    public static class StructurizrJsonTerminologyMap
    {
        public static TerminologyToImport ToTerminologyToImport(this StructurizrJsonTerminology source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new TerminologyToImport
            {
                Person = source.Person,
                SoftwareSystem = source.SoftwareSystem,
                Container = source.Container,
                Component = source.Component,
                DeploymentNode = source.DeploymentNode,
                InfrastructureNode = source.InfrastructureNode,
                Relationship = source.Relationship,
                Code = source.Code,
                Enterprise = source.Enterprise
            };
        }
    }
}
