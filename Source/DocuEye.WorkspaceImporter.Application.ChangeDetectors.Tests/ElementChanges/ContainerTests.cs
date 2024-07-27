using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class ContainerTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenContainerDoNotExistsThenElementWillBeAdded()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenContainerExistsThenElementWillBeUpdated()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenContainerExistsButIsNotInImportThenElementWillBeDeleted() { 
            Assert.Fail();
        }
    }
}
