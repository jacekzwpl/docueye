using DocuEye.Infrastructure.Tests.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.Structurizr.Json.Model.Maps.Tests
{
    public class StructurizrJsonTerminologyMapTests
    {
        [Test]
        public void Mapping_StructurizrJsonTerminology_To_TerminologyToImport_Is_Correct()
        {
            // Arrange
            var terminology = new StructurizrJsonTerminology()
            {
                Person = "User",
                SoftwareSystem = "App",
                Container = "Service",
                Component = "Module",
                DeploymentNode = "Server",
                InfrastructureNode = "VM",
                Relationship = "Link",
                Code = "SourceCode",
                Enterprise = "Org"
            };
            // Act
            var element = terminology.ToTerminologyToImport();
            // Assert
            MappingAssert.AssertMapped(
                terminology, element
            );
        }
    }
}
