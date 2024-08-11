using AutoMapper;
using DocuEye.WorkspaceImporter.Application.Mappings;
using MediatR;
using Moq;

namespace DocuEye.WorkspaceImporter.Application.Tests.Commands
{
    public class BaseCommandTests
    {
        protected IMediator mediator;
        protected IMapper mapper;
        [SetUp]
        public void Setup()
        {
            var mediatorMock = new Mock<IMediator>();
            mediatorMock.Setup(i => i.Publish(It.IsAny<INotification>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            mediatorMock.Setup(i => i.Publish(It.IsAny<IRequest>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);
            this.mediator = mediatorMock.Object;

            MapperConfiguration config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ApiToModelMapingProfile>();
                cfg.AddProfile<ModelToModelMappingProfile>();
            });
            this.mapper = config.CreateMapper();
        }
    }
}
