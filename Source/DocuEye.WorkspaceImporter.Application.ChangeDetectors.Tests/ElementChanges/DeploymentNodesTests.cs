using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class DeploymentNodesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenDeploymentNodeDoNotExistsThenElementWillBeAdded()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenDeploymentNodeExistsThenElementWillBeUpdated()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenDeploymentNodeExistsButIsNotInImportThenElementWillBeDeleted()
        {
            Assert.Fail();
        }
    }
}
