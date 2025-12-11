using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class TerminologyToImportMapTests
    {
        [Test]
        public void Mapping_TerminologyToImport_To_Terminology_Works()
        {
            var source = new TerminologyToImport { 
                Person = "Person", 
                SoftwareSystem = "Software System", 
                Container = "Container", 
                Component = "Component",
                Code = "Code",
                DeploymentNode = "Deployment Node",
                Relationship = "Relationship",
                InfrastructureNode = "Infrastructure Node",
                Enterprise = "Enterprise"
            };
            var result = source.ToTerminology();
            MappingAssert.AssertMapped(source, result);
        }
    }
}
