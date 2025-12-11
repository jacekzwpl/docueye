using DocuEye.WorkspaceImporter.Model;
using MediatR;
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
            mediator.Setup(i => i.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            this.mediator = mediator.Object;

            this.import = new WorkspaceImport()
            {
                Id = Guid.NewGuid().ToString()
            };

        }
    }
}