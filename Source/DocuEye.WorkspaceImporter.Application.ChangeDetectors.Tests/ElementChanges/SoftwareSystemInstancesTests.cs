using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class SoftwareSystemInstancesTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenSoftwareSystemInstanceDoNotExistsThenElementWillBeAdded()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenSoftwareSystemInstanceExistsThenElementWillBeUpdated()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenSoftwareSystemInstanceExistsButIsNotInImportThenElementWillBeDeleted()
        {
            Assert.Fail();
        }
    }
}
