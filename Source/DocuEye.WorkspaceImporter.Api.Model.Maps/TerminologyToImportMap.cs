using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using DocuEye.WorkspacesKeeper.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps
{
    public static class TerminologyToImportMap
    {
        public static Terminology ToTerminology(this TerminologyToImport source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            return new Terminology
            {
                Person = source.Person,
                SoftwareSystem = source.SoftwareSystem,
                Container = source.Container,
                Component = source.Component,
                Code = source.Code,
                DeploymentNode = source.DeploymentNode,
                Relationship = source.Relationship,
                InfrastructureNode = source.InfrastructureNode,
                Enterprise = source.Enterprise
            };
        }

        public static IEnumerable<Terminology> ToTerminologies(this IEnumerable<TerminologyToImport> sources)
        {
            if (sources == null) throw new ArgumentNullException(nameof(sources));
            var result = new List<Terminology>();
            foreach (var s in sources) result.Add(s.ToTerminology());
            return result.ToArray();
        }
    }
}
