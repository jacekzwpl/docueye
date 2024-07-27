using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class ContainerInstancesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenContainerInstanceDoNotExistsThenElementWillBeAdded()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenContainerInstanceExistsThenElementWillBeUpdated()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenContainerInstanceExistsButIsNotInImportThenElementWillBeDeleted()
        {
            Assert.Fail();
        }
    }
}
