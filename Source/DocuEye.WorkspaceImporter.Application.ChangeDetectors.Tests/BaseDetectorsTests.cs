using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.WorkspaceImporter.Model;

using Moq;

namespace DocuEye.WorkspaceImporter.Application.ChangeDetectors.Tests
{
    public class BaseDetectorsTests
    {
        protected IMediator mediator;
        protected WorkspaceImport import;


        [SetUp]
        public void Setup()
        {

            var mediator = new Mock<IMediator>();
            mediator.Setup(i => i.SendEventAsync(It.IsAny<IEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            this.mediator = mediator.Object;

            this.import = new WorkspaceImport()
            {
                Id = Guid.NewGuid().ToString()
            };

        }
    }
}