using DocuEye.Infrastructure.Tests.Common;
using DocuEye.WorkspaceImporter.Api.Model.ViewConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorkspacesKeeperModel = DocuEye.WorkspacesKeeper.Model;

namespace DocuEye.WorkspaceImporter.Api.Model.Maps.Tests
{
    public class ViewConfigurationToImportMapTests
    {
        [Test]
        public void Mapping_ViewConfigurationToImport_To_ViewConfiguration_Works()
        {
            var source = new ViewConfigurationToImport
            {
                Terminology = new TerminologyToImport {
                    Person = "Person",
                    SoftwareSystem = "System",
                    Container = "Container",
                    Component = "Component",
                    Code = "Code",
                    DeploymentNode = "Node",
                    Relationship = "Rel",
                    InfrastructureNode = "Infra"
                },
                ElementStyles = new[]
                {
                    new ElementStyleToImport {  },
                    new ElementStyleToImport {  }
                },
                RelationshipStyles = new[]
                {
                    new RelationshipStyleToImport {  },
                    new RelationshipStyleToImport {  }
                },
                Themes = new[] { "theme1", "theme2" },
                GroupSeparator = "::",

            };

            var result = source.MapToViewConfiguration();

            MappingAssert.AssertMapped(
                source, result,
                ignoreDestProps: new[] {
                    nameof(WorkspacesKeeperModel.ViewConfiguration.ElementStyles),
                    nameof(WorkspacesKeeperModel.ViewConfiguration.RelationshipStyles),
                    nameof(WorkspacesKeeperModel.ViewConfiguration.Terminology),
                    nameof(WorkspacesKeeperModel.ViewConfiguration.Id),
                }
                );
           // MappingAssert.AssertMapped(
            //    source.Terminology, result.Terminology);
            Assert.That(result.ElementStyles.Count(), Is.EqualTo(source.ElementStyles.Count()));
            Assert.That(result.RelationshipStyles.Count(), Is.EqualTo(source.RelationshipStyles.Count()));
        }
    }
}
