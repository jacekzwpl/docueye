using DocuEye.Infrastructure.Mediator.Commands;
using DocuEye.Infrastructure.Mediator.Events;
using DocuEye.Infrastructure.Mediator.Queries;
using DocuEye.Infrastructure.Mediator.Tests.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace DocuEye.Infrastructure.Mediator.Tests
{
    public class BaseTests
    {
        protected ServiceProvider serviceProvider;

        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection();
            // Register mediator
            services.AddSingleton<IMediator, Mediator>();

            services.AddSingleton(new EventHandlingState());

            // Register handlers
            services.AddTransient<ICommandHandler<TestCommand, TestResult>, TestCommandHandler>();
            services.AddTransient<IQueryHandler<TestQuery, TestResult>, TestQueryHandler>();
            services.AddTransient<IEventHandler<TestEvent>, TestEventHandler>();
            services.AddTransient<IEventHandler<TestEvent>, SecondEventHandler>();

            serviceProvider = services.BuildServiceProvider();
        }

        [TearDown]
        public void TearDown()
        {
            serviceProvider?.Dispose();
        }

        
    }
}