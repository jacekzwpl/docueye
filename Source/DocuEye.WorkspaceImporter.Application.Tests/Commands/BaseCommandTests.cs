
using DocuEye.Infrastructure.Mediator;
using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Events;
using Moq;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class BaseCommandTests
    {
        protected IMediator mediator;
        [SetUp]
        public void Setup()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(i => i.SendEventAsync(It.IsAny<IEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            mediatorMock.Setup(i => i.SendCommandAsync(It.IsAny<ICommand>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            this.mediator = mediatorMock.Object;
        }
    }
}
