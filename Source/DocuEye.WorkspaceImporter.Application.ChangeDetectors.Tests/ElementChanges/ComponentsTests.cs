using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests.ElementChanges
{
    public class ComponentsTests : BaseDetectorsTests
    {
        [Test]
        public async Task WhenComponentDoNotExistsThenElementWillBeAdded()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenComponentExistsThenElementWillBeUpdated()
        {
            Assert.Fail();
        }

        [Test]
        public async Task WhenComponentExistsButIsNotInImportThenElementWillBeDeleted()
        {
            Assert.Fail();
        }
    }
}
