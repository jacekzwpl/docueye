using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class InfrastructureNodesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenInfrastructureNodeDoNotExistsThenElementWillBeAdded()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenInfrastructureNodeExistsThenElementWillBeUpdated()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenInfrastructureNodeExistsButIsNotInImportThenElementWillBeDeleted()
        {
            Assert.Fail();
        }
    }
}
